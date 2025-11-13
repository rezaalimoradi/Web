using CMS.Domain.Common;
using CMS.Infrastructure.Events;

namespace CMS.Infrastructure.Persistence
{
    /// <summary>
    /// Unit of Work implementation for managing database transactions.
    /// Implements IUnitOfWork pattern to commit changes to the database.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly DomainEventDispatcher _dispatcher;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class with the given DbContext.
        /// </summary>
        /// <param name="context">The database context to be used by the unit of work.</param>
        public UnitOfWork(AppDbContext context, DomainEventDispatcher dispatcher)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new EntityRepository<T>(_context);
        }

        /// <summary>
        /// Asynchronously saves all changes made in the unit of work to the database.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The number of affected rows.</returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);

            await DispatchDomainEventsAsync();

            return result;
        }

        /// <summary>
        /// Saves all changes made in the unit of work to the database synchronously.
        /// </summary>
        /// <returns>The number of affected rows.</returns>
        public int SaveChanges()
        {
            var result = _context.SaveChanges();

            DispatchDomainEventsAsync().GetAwaiter().GetResult();

            return result;
        }

        private async Task DispatchDomainEventsAsync()
        {
            var aggregates = _context.ChangeTracker
                                     .Entries<AggregateRoot>()
                                     .Where(e => e.Entity.DomainEvents.Any())
                                     .Select(e => e.Entity)
                                     .ToList();

            foreach (var aggregate in aggregates)
            {
                await _dispatcher.DispatchEventsAsync(aggregate);
            }
        }

        /// <summary>
        /// Disposes the unit of work, releasing the database context.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
