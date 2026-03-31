using EventAssos.Core.DTOs.Requests;
using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Core.Interfaces.Services.Data;
using EventAssos.Core.Objects;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Services.Data
{
    public class EventService(IEventRepository _eventRepository) : IEventService
    {
        public async Task<ResultPattern<Event>> CancelAsync(Guid eventId)//Avec ResultPattern
        {
            var eve = await _eventRepository.GetByIdAsync(eventId);
            if (eve == null) return ResultPattern<Event>.Failure("Événement introuvable.");
            if (eve.Statut != EventStatut.EnCours)
                return ResultPattern<Event>.Failure("Seuls les événements 'en cours' peuvent être annulés.");
            eve.Statut = EventStatut.Annulé;
            await _eventRepository.UpdateAsync(eve);
            return ResultPattern<Event>.Success(eve);
        }

        public async Task DeleteAsync(Guid eventId)
        {
            var eve= await _eventRepository.GetByIdAsync(eventId) 
                ?? throw new KeyNotFoundException("Événement introuvable.");

            if (eve.Statut != EventStatut.EnAttente)
                throw new InvalidOperationException("Seuls les événements 'en attente' peuvent être supprimés.");

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
            if (eventdto.Lieu is not null) eve.lieu = eventdto.Lieu;
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
                eve.start = eventdto.Start.Value;
            }

            //le end doit être après le start
            if (eventdto.End.HasValue)
            {
                if (eventdto.End.Value <= eventdto.Start)
                    throw new InvalidOperationException("La date de fin doit être postérieure à la date de début.");
                eve.end = eventdto.End.Value;
            }

            //la date limite de l'iscription doit être avant le start
            if (eventdto.LimiteInscription.HasValue)
            {
                if (eventdto.LimiteInscription.Value > eve.start)
                    throw new InvalidOperationException("La date limite d'inscription doit être antérieure ou égale à la date de début.");
                eve.LimiteInscription = eventdto.LimiteInscription.Value;
            }

            //Mise à jour
            eve.MiseAJour = DateTime.UtcNow;

            await _eventRepository.UpdateAsync(eve);
            return eve;
        }
    }
}
