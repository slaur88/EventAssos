using EventAssos.Core.DTOs.Requests;
using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Data
{
    public interface IEventService: IBaseService<Event, Guid>
    {
        Task<Event> UpdateAsync(Guid eventId, UpdateEventRequestDTO eventdto);
    }
}
