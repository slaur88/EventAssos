using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventAssos.Core.Interfaces.Repositories
{
    //Creation du CRUD Générique
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : struct
   
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        


        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync( TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
