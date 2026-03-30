using EventAssos.Core.DTOs.Requests;
using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Core.Interfaces.Services.Data;
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
        public async Task DeleteAsync(Guid eventId)
        {
            var eve= await _eventRepository.GetByIdAsync(eventId) 
                ?? throw new KeyNotFoundException("Événement introuvable.");

            if (eve.Statut != EventStatut.EnAttente)
                throw new InvalidOperationException("Seuls les événements 'en attente' peuvent être supprimés.");

            await _eventRepository.DeleteAsync(eventId);
        }

        public Task<Event> UpdateAsync(Guid eventId, UpdateEventRequestDTO eventdto)
        {
            throw new NotImplementedException();
        }
    }
}
