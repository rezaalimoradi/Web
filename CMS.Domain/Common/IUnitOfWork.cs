namespace CMS.Domain.Common
{
    /// <summary>
    /// Defines the contract for a unit of work pattern.
    /// A unit of work is used to manage database transactions and ensure that all changes to the database are made atomically.
    /// It allows saving changes either asynchronously or synchronously.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;

        /// <summary>
        /// Asynchronously saves all changes made in the unit of work to the database.
        /// This method commits the transaction and returns the number of affected rows.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Task that represents the asynchronous operation, with a result of the number of affected rows.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Synchronously saves all changes made in the unit of work to the database.
        /// This method commits the transaction and returns the number of affected rows.
        /// </summary>
        /// <returns>The number of affected rows.</returns>
        int SaveChanges();
    }
}
