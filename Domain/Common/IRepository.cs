using System.Linq.Expressions;

namespace CMS.Domain.Common
{
    /// <summary>
    /// IRepository interface defines the contract for repository operations for a specific entity type (TEntity).
    /// It extends basic CRUD functionality, including asynchronous methods and pagination.
    /// </summary>
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Asynchronously retrieves a single entity by a filter, including specified navigation properties.
        /// </summary>
        /// <param name="predicate">Filter expression to find the entity.</param>
        /// <param name="includes">Navigation properties to include.</param>
        /// <param name="includeDeleted">Whether to include deleted entities.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null, bool includeDeleted = true);

        Task<TEntity?> GetByIdAsync(long id, bool includeDeleted = true);

        /// <summary>
        /// Asynchronously retrieves multiple entities by their IDs, including optionally deleted entities.
        /// </summary>
        /// <param name="ids">The list of IDs of the entities to retrieve.</param>
        /// <param name="includeDeleted">Whether to include deleted entities in the result.</param>
        /// <returns>Task containing a list of entities of type TEntity.</returns>
        Task<IList<TEntity>> GetByIdsAsync(IList<long> ids, bool includeDeleted = true);

        /// <summary>
        /// Asynchronously retrieves paged data, with optional filtering, pagination parameters, and option to include deleted entities.
        /// </summary>
        /// <param name="func">An optional filtering function for the query.</param>
        /// <param name="pageIndex">The index of the page to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="getOnlyTotalCount">Whether to only retrieve the total count of items.</param>
        /// <param name="includeDeleted">Whether to include deleted entities in the result.</param>
        /// <returns>Task containing the paged list of entities of type TEntity.</returns>
        Task<IPagedList<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true);

        /// <summary>
        /// Asynchronously inserts a new entity into the repository, with the option to publish an event.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="publishEvent">Whether to publish an event after insertion.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Asynchronously inserts multiple entities into the repository, with the option to publish an event.
        /// </summary>
        /// <param name="entities">The list of entities to insert.</param>
        /// <param name="publishEvent">Whether to publish an event after insertion.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task InsertAsync(IList<TEntity> entities);

        /// <summary>
        /// Updates an existing entity in the repository, with the option to publish an event.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="publishEvent">Whether to publish an event after updating.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates multiple entities in the repository, with the option to publish an event.
        /// </summary>
        /// <param name="entities">The list of entities to update.</param>
        /// <param name="publishEvent">Whether to publish an event after updating.</param>
        void Update(IList<TEntity> entities);

        /// <summary>
        /// Deletes an entity from the repository, with the option to publish an event.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="publishEvent">Whether to publish an event after deletion.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes multiple entities from the repository, with the option to publish an event.
        /// </summary>
        /// <param name="entities">The list of entities to delete.</param>
        /// <param name="publishEvent">Whether to publish an event after deletion.</param>
        void Delete(IList<TEntity> entities);

        /// <summary>
        /// Gets the queryable collection of entities in the repository.
        /// </summary>
        IQueryable<TEntity> Table { get; }
    }
}
