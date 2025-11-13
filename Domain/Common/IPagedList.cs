namespace CMS.Domain.Common
{
    /// <summary>
    /// Paged list interface
    /// </summary>
    public partial interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// The index of the current page.
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// The total number of items in the data source.
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// The total number of pages, calculated based on the total count and page size.
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// A boolean indicating if there is a previous page.
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// A boolean indicating if there is a next page.
        /// </summary>
        bool HasNextPage { get; }
    }
}
