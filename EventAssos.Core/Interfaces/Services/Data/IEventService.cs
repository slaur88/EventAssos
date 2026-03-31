using EventAssos.Secu.DTOs.Requests;
using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Interfaces.Services.Data
{
    public interface IEventService: IBaseService<Event, Guid>
    {
        Task<Event> UpdateAsync(Guid eventId, UpdateEventRequestDTO eventdto);
    }
}
