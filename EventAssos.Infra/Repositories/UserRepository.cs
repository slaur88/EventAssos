using EventAssos.Core.Interfaces.Repositories;
using EventAssos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using EventAssos.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Infra.Repositories
{
    public class UserRepository(EventAssosContext _context)
        : BaseRepository<User, Guid>(_context), IUserRepository
        
    {
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _entities.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByPseudoAsync(string pseudo)
        {
            return await _entities.FirstOrDefaultAsync(u => u.Pseudo == pseudo);
        }
    }
}
