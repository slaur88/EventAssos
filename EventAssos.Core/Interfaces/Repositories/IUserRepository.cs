using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Secu.Interfaces.Repositories
{    
    //Création de l'interface qui sera implémentée après
    public interface IUserRepository: IBaseRepository<User, Guid>
    {
        Task<User?> GetByEmailOrPseudoAsync(string identifier);

        Task<User?> GetUserByPseudoAsync(string pseudo);

        Task<User?> GetUserByEmailAsync(string email);

        
    }
}
