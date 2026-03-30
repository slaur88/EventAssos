using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace EventAssos.Core.Interfaces.Repositories
{
    public interface IEventRepository : IBaseRepository<Event, Guid>
    {
        Task<Event?> GetByIdWithDetailsAsync(Guid id); 
        
    }
}
