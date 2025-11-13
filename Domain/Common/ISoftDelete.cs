namespace CMS.Domain.Common
{
    /// <summary>
    /// Interface for soft delete functionality. Entities that implement this interface 
    /// can be marked as deleted without being removed from the database.
    /// </summary>
    public partial interface ISoftDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when the entity was soft deleted.
        /// </summary>
        DateTime? DeletedAt { get; set; }
    }
}
