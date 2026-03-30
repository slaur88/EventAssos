using EventAssos.Core.DTOs.Requests;
using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Core.Interfaces.Services.Data;
using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Services.Data
{
    public class EventService(IEventRepository _eventRepository) : IEventService
    {
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> UpdateAsync(Guid eventId, UpdateEventRequestDTO eventdto)
        {
            throw new NotImplementedException();
        }
    }
}
