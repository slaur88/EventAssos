using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using EventAssos.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventAssos.Infra.Repositories
{

    public class EventRepository(EventAssosContext _context)
        : BaseRepository<Event, Guid>(_context), IEventRepository
    {
        public Task<Event?> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
