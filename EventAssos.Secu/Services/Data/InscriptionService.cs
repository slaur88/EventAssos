using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Core.Interfaces.Services.Data;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using EventAssos.Secu.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Services.Data
{

    public class InscriptionService(
         IInscriptionRepository _inscriptionRepository,
         IEventRepository _eventRepository
     ) : IInscriptionService
    {
        public async Task<string> InscriptionMemberAsync(Guid userId, Guid eventId)
        {
            // Récupération de l'événement avec ses inscrits

            var ev = await _eventRepository.GetByIdWithDetailsAsync(eventId);

            if (ev == null) throw new KeyNotFoundException("Événement non trouvé.");

            // Statut "En attente"
            if (ev.Statut != EventStatut.EnAttente)
                throw new InvalidOperationException("L'événement n'est plus ouvert.");

            //Date limite
            if (ev.LimiteInscription < DateTime.Now)
                throw new InvalidOperationException("La date limite est dépassée.");

            // Déjà inscrit ?
            // On vérifie dans la liste des inscriptions de l'événement chargé
            var dejaInscrit= ev.Inscriptions.Any(i => i.UserId == userId);
            if (dejaInscrit)
                throw new InvalidOperationException("Vous êtes déjà inscrit.");

            //  Places et Liste d'attente
            bool dansListeAttente = false;

            // On compte ceux qui ne sont PAS en liste d'attente
            int Inscrits = ev.Inscriptions.Count(i => !i.listeAttenteActive);

            if (Inscrits >= ev.NbMax)
            {
                if (ev.ListeAttenteActive)
                {
                    dansListeAttente = true;
                }
                else
                {
                    throw new InvalidOperationException("L'événement est complet.");
                }
            }

            
            var newInscription = new Inscription
            {
                InscriptionId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Date = DateTime.Now,
                listeAttenteActive = dansListeAttente
            };

            await _inscriptionRepository.AddAsync(newInscription);

            return dansListeAttente ? "Ajouté à la liste d'attente" : "Inscription réussie";
        }
    }
}
