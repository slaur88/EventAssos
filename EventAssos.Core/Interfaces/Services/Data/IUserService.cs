using EventAssos.Core.DTOs.Requests;
using EventAssos.Core.DTOs.Responses;
using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Data
{
    public interface IUserService : IBaseService<User, Guid>
    {
        Task UpdatePseudo(Guid userId, string newPseudo);
        Task<User?> GetByIdAsync(Guid id);
    }
}
