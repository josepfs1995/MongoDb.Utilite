using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDb.Util.Interfaces{
    
    public interface IDbSet<TEntity> where TEntity: class{
        /// <summary>
        /// Gets all collection.
        /// </summary>
        IEnumerable<TEntity> ToList();
        /// <summary>
        /// Gets all collection with filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        IEnumerable<TEntity> ToList(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// Get first collection.
        /// </summary>
        TEntity FirstOrDefault();
        /// <summary>
        /// Get first collection with filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// Add collection.
        /// </summary>
        /// <param name="model">The entities.</param>
        void Add(TEntity model);
        /// <summary>
        /// Update collection with UniqueIdentifier
        /// </summary>
        /// <param name="model">The entities.</param>
        void Update(TEntity model);
        /// <summary>
        /// Update collection with filter
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="model">The entities.</param>
        void Update(Expression<Func<TEntity, bool>> filter, TEntity model);
        /// <summary>
        /// Delete collection with filter
        /// </summary>
        /// <param name="filter">The filter.</param>
        void Delete(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets all collection.
        /// </summary>
        Task<IEnumerable<TEntity>> ToListAsync();
        /// <summary>
        /// Gets all collection with filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> filter);
         /// <summary>
        /// Get first collection.
        /// </summary>
        Task<TEntity> FirstOrDefaultAsync();
        /// <summary>
        /// Get first collection with filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// Add collection.
        /// </summary>
        /// <param name="model">The entities.</param>
        Task AddAsync(TEntity model);
        /// <summary>
        /// Update collection with UniqueIdentifier
        /// </summary>
        /// <param name="model">The entities.</param>
        Task UpdateAsync(TEntity model);
        /// <summary>
        /// Update collection with filter
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="model">The entities.</param>
        Task UpdateAsync(Expression<Func<TEntity, bool>> filter,TEntity model);
        /// <summary>
        /// Delete collection with filter
        /// </summary>
        /// <param name="filter">The filter.</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> filter);
    }
}