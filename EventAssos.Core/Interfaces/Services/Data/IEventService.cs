using EventAssos.Secu.DTOs.Requests;
using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using EventAssos.Core.Objects;
using EventAssos.Core.DTOs.Responses;

namespace EventAssos.Secu.Interfaces.Services.Data;

public interface IEventService: IBaseService<Event, Guid>
{
    Task<Event> UpdateAsync(Guid eventId, UpdateEventRequestDTO eventdto);
    Task<ResultPattern<Event>> CancelAsync(Guid eventId);

    Task<ResultPattern<Event>> CreateEventAsync(AddEventRequestDto eventdto);

    Task<ResultPattern<EventStatsResponseDTO>> GetStatsAsync(Guid eventId);
}
