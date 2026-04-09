using EventAssos.Secu.DTOs.Requests;
using EventAssos.Secu.DTOs.Responses;
using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Interfaces.Services.Data
{
    public interface IUserService : IBaseService<User, Guid>
    {
        Task UpdatePseudo(Guid userId, string newPseudo);
        Task<User?> GetByIdAsync(Guid id);

        Task AddAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);


    }
}
