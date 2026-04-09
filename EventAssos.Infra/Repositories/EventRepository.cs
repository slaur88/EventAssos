using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using EventAssos.Infra.Database.Context;
using EventAssos.Secu.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventAssos.Infra.Repositories
{

    public class EventRepository(EventAssosContext _context)
        : BaseRepository<Event, Guid>(_context), IEventRepository
    {
        public async Task<Event?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Events.Include(e => e.Inscriptions) //Pour récupérer les inscriptions 
                .Include(e => e.Categories) //Pour récupérer les catégories
                .FirstOrDefaultAsync(e => e.Id == id);

        }
        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events
                .Include(e => e.Categories) 
                .ToListAsync();
        }

    }
}
