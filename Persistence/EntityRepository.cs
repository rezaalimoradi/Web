using CMS.Domain.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Linq.Expressions;

namespace Persistence
{
    public partial class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EntityRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = AddDeletedFilter(Table, includeDeleted);

            foreach (var includeExpression in includes)
            {
                query = query.Include(includeExpression);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = true, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (!includeDeleted)
            {
                query = AddDeletedFilter(Table, includeDeleted);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetByIdAsync(long id, bool includeDeleted = true)
        {
            var query = AddDeletedFilter(Table, includeDeleted);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Retrieves all entities with optional filtering, ordering, and pagination.
        /// </summary>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null, bool includeDeleted = true)
        {
            var query = AddDeletedFilter(Table, includeDeleted);

            if (func != null)
                query = func(query);

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves all entities with pagination.
        /// </summary>
        public async Task<IPagedList<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true)
        {
            var query = AddDeletedFilter(Table, includeDeleted);

            if (func != null)
                query = func(query);

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        public async Task<TEntity?> GetByIdAsync(long? id, bool includeDeleted = true)
        {
            if (!id.HasValue || id.Value == 0)
                return null;

            var query = AddDeletedFilter(Table, includeDeleted);

            return await query.FirstOrDefaultAsync(entity => entity.Id == id.Value);
        }

        /// <summary>
        /// Retrieves entities by a list of unique identifiers.
        /// </summary>
        public async Task<IList<TEntity>> GetByIdsAsync(IList<long> ids, bool includeDeleted = true)
        {
            if (ids?.Any() != true)
                return new List<TEntity>();

            var query = AddDeletedFilter(Table, includeDeleted);

            return await query.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        /// <summary>
        /// Inserts a new entity into the database.
        /// </summary>
        public async Task InsertAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Inserts multiple entities into the database.
        /// </summary>
        public async Task InsertAsync(IList<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));
            await _dbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        public void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            entity.SetUpdatedAt(DateTime.Now);
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Updates multiple entities in the database.
        /// </summary>
        public void Update(IList<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));

            var updatedDate = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.SetUpdatedAt(updatedDate);
            }

            _dbSet.UpdateRange(entities);
        }

        /// <summary>
        /// Deletes an entity from the database. If the entity implements ISoftDeletable, it will be soft-deleted.
        /// </summary>
        public void Delete(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            if (entity is ISoftDeletable softDeletable)
            {
                softDeletable.Deleted = true;
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Deletes multiple entities from the database. Soft-delete is applied for ISoftDeletable entities.
        /// </summary>
        public void Delete(IList<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));

            if (!entities.Any()) return;

            if (typeof(TEntity).GetInterface(nameof(ISoftDeletable)) == null)
            {
                _dbSet.RemoveRange(entities);
            }
            else
            {
                foreach (var entity in entities)
                {
                    if (entity is ISoftDeletable softDeletable)
                    {
                        softDeletable.Deleted = true;
                    }
                }
                _dbSet.UpdateRange(entities);
            }
        }

        /// <summary>
        /// Provides a queryable table of entities for advanced querying.
        /// </summary>
        public virtual IQueryable<TEntity> Table => _dbSet.AsQueryable();

        /// <summary>
        /// Adds a filter for soft-deleted entities if required.
        /// </summary>
        protected virtual IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, bool includeDeleted)
        {
            if (!includeDeleted && typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {
                return query.Where(e => !((ISoftDeletable)e).Deleted);
            }

            return query;
        }
    }
}
