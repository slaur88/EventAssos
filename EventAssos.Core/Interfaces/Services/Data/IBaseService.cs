using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Data
{
   public interface IBaseService<TEntity, TKey>
    where TEntity : class
    where TKey : struct 
    {
        Task DeleteAsync(TKey id);
    }
}
