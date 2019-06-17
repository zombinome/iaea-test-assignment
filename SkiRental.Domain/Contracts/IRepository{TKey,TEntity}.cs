using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkiRental.Domain.Contracts
{
    public interface IRepository<TKey, TEntity>
    {
        ITransactionScope BeginScope();

        /// <summary>
        /// Returns entity by its id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity with requested <paramref name="id"/> or null if there was no entity with requested <paramref name="id"/></returns>
        Task<TEntity> GetAsync(TKey id);

        /// <summary>
        /// Returns
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, int skip = 0, int? take = null);

        /// <summary>
        /// Save new or modified entity to database
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <returns>Saved entity or null, if entity to update was not found in database</returns>
        Task<TEntity> SaveAsync(TEntity entity);

        /// <summary>
        /// Removes entity from database
        /// </summary>
        /// <param name="entityId">Entity id</param>
        /// <returns>True if entity was found and deleted from datadbase, false otherwise</returns>
        Task<bool> DeleteAsync(TKey entityId);
    }
}
