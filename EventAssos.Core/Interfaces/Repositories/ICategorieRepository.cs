using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Repositories
{
    public interface ICategorieRepository
    {
        Task<IEnumerable<Categorie>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
