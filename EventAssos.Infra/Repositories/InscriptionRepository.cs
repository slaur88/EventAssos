using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Domain.Entities;
using EventAssos.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EventAssos.Infra.Repositories
{
    public class InscriptionRepository : BaseRepository<Inscription, Guid>, IInscriptionRepository
    {
        public InscriptionRepository(EventAssosContext _context) : base(_context)
        {
        }

        public async Task<Inscription?> GetByUserAndEventAsync(Guid userId, Guid eventId)
        {
            // On utilise _entities qui vient du BaseRepository
            return await _entities.FirstOrDefaultAsync(i => i.UserId == userId && i.EventId == eventId);
        }
    }
}
