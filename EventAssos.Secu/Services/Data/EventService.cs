using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Core.Objects;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using EventAssos.Secu.DTOs.Requests;
using EventAssos.Secu.Interfaces.Repositories;
using EventAssos.Secu.Interfaces.Services.Data;
using EventAssos.Secu.Interfaces.Services.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Services.Data;


    public class EventService(IEventRepository _eventRepository,
        ICategorieRepository _categorieRepository, IUserRepository _userRepository, IEmailService _emailService) : IEventService
{
    public async Task<ResultPattern<Event>> CreateEventAsync(AddEventRequestDto eventdto)
    {
        var admin = await _userRepository.GetUserByEmailAsync("dupont@admin.com");//on reprend le mail de Madame 

        //le start doit être dans le futur
        if (eventdto.Start <= DateTime.UtcNow)
            return ResultPattern<Event>.Failure("La date de début doit être postérieure à aujourd'hui.");

        //end après start
        if (eventdto.End <= eventdto.Start)
            return ResultPattern<Event>.Failure("La date de fin doit être postérieure à la date de début.");

        //LimiteInscription doit être antérieure ou égale à Start
        if (eventdto.LimiteInscription > eventdto.Start)
            return ResultPattern<Event>.Failure("La date limite de l'inscriptiondoit être antérieure à la date de début.");

        //NbMin doit être inférieur ou égal à NbMax
        if (eventdto.NbMin > eventdto.NbMax)
            return ResultPattern<Event>.Failure("Le nombre minimum doit être inférieur ou égal au maximum.");

        var newEvent = new Event
        {
            Id = Guid.NewGuid(),
            Name = eventdto.Name,
            Description = eventdto.Description,
            Lieu = eventdto.Lieu,
            Start = eventdto.Start,
            End = eventdto.End,
            NbMin = eventdto.NbMin,
            NbMax = eventdto.NbMax,
            Statut = EventStatut.EnAttente,
            CreationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            LimiteInscription = eventdto.LimiteInscription,
            MiseAJour = DateTime.UtcNow,
            ListeAttenteActive = eventdto.ListeAttenteActive,
            CreatedByUserId = admin.Id


        };

        if (eventdto.Img != null)
        {
            using var ms = new MemoryStream();
            await eventdto.Img.CopyToAsync(ms);
            newEvent.Img = ms.ToArray();
        }

        var categories = await _categorieRepository.GetByIdsAsync(eventdto.CategorieIds);

        newEvent.Categories = categories.ToList();


        var result = await _eventRepository.AddAsync(newEvent);


        
        var allUsers = await _userRepository.GetAllAsync();

       //on ne lance la boucle que s'il y a des gens
        if (allUsers != null && allUsers.Any())
        {
            foreach (var user in allUsers)
            {
                // on n'envoie pas de mail si l'utilisateur n'a pas d'adresse
                if (string.IsNullOrEmpty(user.Email)) continue;

                try
                {
                    string subject = $"Nouvel événement : {newEvent.Name}";
                    string body = $@"
                    <html>
                        <body>
                            <h2>Nouvel événement disponible !</h2>
                            <p>Un nouvel événement vient d'être ajouté sur EventAssos !</p>
                            <p><strong>{newEvent.Name}</strong></p>
                            <p>{newEvent.Description}</p>
                            <p>Date : {newEvent.Start}</p>
                            <p>Lieu : {newEvent.Lieu ?? "À définir"}</p>
                        </body>
                    </html>";

                    await _emailService.SendEmailAsync(user.Email, subject, body);
                }
                catch (Exception ex)
                {
                    // Si l'envoi échoue (pas d'internet, service mail en panne),
                    // on écrit l'erreur dans la console mais on ne fait pas planter l'API.
                    Console.WriteLine($"Échec envoi mail à {user.Email} : {ex.Message}");
                }
            }
        }


        return ResultPattern<Event>.Success(result);
    }

    public async Task<ResultPattern<Event>> CancelAsync(Guid eventId)//Avec ResultPattern
    {
        var eve = await _eventRepository.GetByIdAsync(eventId);
        if (eve == null) return ResultPattern<Event>.Failure("Événement introuvable.");
        if (eve.Statut != EventStatut.EnCours)
            return ResultPattern<Event>.Failure("Seuls les événements 'en cours' peuvent être annulés.");
        eve.Statut = EventStatut.Annulé;
        await _eventRepository.UpdateAsync(eve);

        var inscrit = eve.Inscriptions.Where(i => i.Statut == StatutInscription.Confirme).ToList();

        foreach (var inscription in inscrit) 
        {
            string subject = $"Événement annulé : {eve.Name}";

            string body = $@"
                <html>
                    <body>
                        <h2> Événement annulé </h2>
                        <p>L'événement <strong>{eve.Name}</strong> a été annulé.</p>
                        <p>Nous sommes désolés pour ce désagrément. Restez à l'affût des prochains événements !</p>
                    </body>
                </html>";
            await _emailService.SendEmailAsync(inscription.User.Email, subject, body);
        }
        return ResultPattern<Event>.Success(eve);
    }

    public async Task DeleteAsync(Guid eventId)
    {
        var eve = await _eventRepository.GetByIdAsync(eventId)
            ?? throw new KeyNotFoundException("Événement introuvable.");

        if (eve.Statut != EventStatut.EnAttente)
            throw new InvalidOperationException("Seuls les événements 'en attente' peuvent être supprimés.");

        var inscrit = eve.Inscriptions.Where(i => i.Statut == StatutInscription.Confirme).ToList();

        foreach (var inscription in inscrit)
        {
            string subject = $"Événement supprimé : {eve.Name}";

            string body = $@"
                <html>
                    <body>
                        <h2> Événement supprimé </h2>
                        <p>L'événement <strong>{eve.Name}</strong> a été supprimé.</p>
                        <p>Nous sommes désolés pour ce désagrément. Restez à l'affût des prochains événements !</p>
                    </body>
                </html>";
            await _emailService.SendEmailAsync(inscription.User.Email, subject, body);
        }
    
        await _eventRepository.DeleteAsync(eventId);
    }

    public async Task<Event> UpdateAsync(Guid eventId, UpdateEventRequestDTO eventdto)
    {
        var eve = await _eventRepository.GetByIdWithDetailsAsync(eventId)
            ?? throw new KeyNotFoundException("Événement introuvable.");

        if (eve.Statut != EventStatut.EnAttente)
            throw new InvalidOperationException("Seuls les événements 'en attente' peuvent être modifiés.");

        if (eventdto.Name != null) eve.Name = eventdto.Name;
        if (eventdto.Description != null) eve.Description = eventdto.Description;
        if (eventdto.Lieu is not null) eve.Lieu = eventdto.Lieu;
        if (eventdto.ListeAttenteActive is not null) eve.ListeAttenteActive = eventdto.ListeAttenteActive.Value;


        //NbMax peut pas déscendre en dessous du nombres d'inscrits
        if (eventdto.NbMax.HasValue)
        {
            var nbInscrits = eve.Inscriptions.Count(i => i.Statut == StatutInscription.Confirme);
            if (eventdto.NbMax.Value < nbInscrits)
                throw new InvalidOperationException(
                    $"Impossible de réduire le maximum en dessous du nombre d'inscrits actuels ({nbInscrits}).");
            eve.NbMax = eventdto.NbMax.Value;
        }

        //mise à jour de NbMin
        if (eventdto.NbMin.HasValue) eve.NbMin = eventdto.NbMin.Value;


        //Le nombre minimum doit être inférieur ou égal au maximum.
        if (eve.NbMin > eve.NbMax)
            throw new InvalidOperationException("Le nombre minimum doit être inférieur ou égal au maximum.");

        //le start doit être dans le futur
        if (eventdto.Start.HasValue)
        {
            if (eventdto.Start.Value <= DateTime.UtcNow)
                throw new InvalidOperationException("La date de début doit être postérieure à aujourd'hui.");
            eve.Start = eventdto.Start.Value;
        }

        //le end doit être après le start
        if (eventdto.End.HasValue)
        {
            if (eventdto.End.Value <= eve.Start)
                throw new InvalidOperationException("La date de fin doit être postérieure à la date de début.");
            eve.End = eventdto.End.Value;
        }

        //la date limite de l'iscription doit être avant le start
        if (eventdto.LimiteInscription.HasValue)
        {
            if (eventdto.LimiteInscription.Value > eve.Start)
                throw new InvalidOperationException("La date limite d'inscription doit être antérieure ou égale à la date de début.");
            eve.LimiteInscription = eventdto.LimiteInscription.Value;
        }

        //Mise à jour
        eve.MiseAJour = DateTime.UtcNow;

        await _eventRepository.UpdateAsync(eve);
        var inscrits = eve.Inscriptions.Where(i => i.Statut == StatutInscription.Confirme);

        foreach (var inscription in inscrits)
        {
            var user = inscription.User;

            string subject = $"Événement modifié : {eve.Name}";

            string body = $@"
                <html>
                    <body>
                        <h2>Événement modifié</h2>
                        <p>L'événement <strong>{eve.Name}</strong> a été mis à jour.</p>
                    </body>
                </html>";

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }
        return eve;
    }
}
