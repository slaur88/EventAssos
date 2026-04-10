using EventAssos.Core.DTOs.Responses;
using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace EventAssos.Secu.Interfaces.Repositories;

public interface IEventRepository : IBaseRepository<Event, Guid>
{
    Task<Event?> GetByIdWithDetailsAsync(Guid id); 

    Task<EventStatsResponseDTO> GetEventStatsAsync(Guid eventId);

}
