using EventAssos.Domain.Entities;
using EventAssos.Secu.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Repositories
{
    public interface IInscriptionRepository: IBaseRepository<Inscription, Guid>
    {
        public interface IInscriptionRepository
        {
            //Pour vérifier si un utilisateur est déjà lié à un événement
            Task<Inscription?> GetByUserAndEventAsync(Guid userId, Guid eventId);
        }
    }
}
