using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Domain.Entities;
using EventAssos.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Infra.Repositories
{
    public class CategorieRepository (EventAssosContext _context) 
        : BaseRepository<Categorie, Guid>(_context), ICategorieRepository
    {
        public async Task<IEnumerable<Categorie>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Categories
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();

        }
    }
}
