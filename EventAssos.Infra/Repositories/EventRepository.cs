using EventAssos.Core.DTOs.Responses;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using EventAssos.Infra.Database.Context;
using EventAssos.Secu.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventAssos.Infra.Repositories;


public class EventRepository(EventAssosContext _context)
    : BaseRepository<Event, Guid>(_context), IEventRepository
{
    public async Task<Event?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Events.Include(e => e.Inscriptions) //Pour récupérer les inscriptions 
            .Include(e => e.Categories) //Pour récupérer les catégories
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<EventStatsResponseDTO?> GetEventStatsAsync(Guid eventId)
    {
        return await _context.Events
            .Where(e => e.Id == eventId)
            .Select(e => new EventStatsResponseDTO
            {
                EventId = e.Id,
                EventName = e.Name,
                NbMax = e.NbMax,
                NbMin = e.NbMin,
                NbConfirmes = e.Inscriptions.Count(i => i.Statut == StatutInscription.Confirme),
                NbListeAttente = e.Inscriptions.Count(i => i.Statut == StatutInscription.ListeAttente),
                TotalInscrits = e.Inscriptions.Count(i => i.Statut != StatutInscription.PasInscrit),
                InscriptionsParJour = e.Inscriptions
                    .GroupBy(i => i.Date.Date)
                    .Select(g => new InscriptionParJourDTO
                    {
                        Date = g.Key,
                        NbInscriptions = g.Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }
}