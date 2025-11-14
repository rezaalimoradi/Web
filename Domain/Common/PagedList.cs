namespace Domain.Common
{
    /// <summary>
    /// The PagedList class is used to manage paginated data, inheriting from List<T> 
    /// and implementing IPagedList<T>.
    /// </summary>
    public partial class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Constructor of the PagedList class that takes a list of data and pagination 
        /// information as parameters.
        /// </summary>
        public PagedList(IList<T> source, int pageIndex, int pageSize, int? totalCount = null)
        {
            /// <summary>
            /// Ensures that pageSize is at least 1 to avoid division by zero.
            /// </summary>
            pageSize = Math.Max(pageSize, 1);

            /// <summary>
            /// If totalCount is not provided, it uses the count of the source list.
            /// </summary>
            TotalCount = totalCount ?? source.Count;

            /// <summary>
            /// Calculates the total number of pages by dividing TotalCount by pageSize.
            /// </summary>
            TotalPages = TotalCount / pageSize;

            /// <summary>
            /// If there is a remainder when dividing, increment TotalPages.
            /// </summary>
            if (TotalCount % pageSize > 0)
                TotalPages++;

            /// <summary>
            /// Assigns the provided pageSize and pageIndex.
            /// </summary>
            PageSize = pageSize;
            PageIndex = pageIndex;

            /// <summary>
            /// Adds the appropriate range of items to the list (either from the source or paginated data).
            /// </summary>
            AddRange(totalCount != null ? source : source.Skip(pageIndex * pageSize).Take(pageSize));
        }

        /// <summary>
        /// The index of the current page.
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// The total number of items in the data source.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// The total number of pages, calculated based on the total count and page size.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// A boolean indicating if there is a previous page.
        /// </summary>
        public bool HasPreviousPage => PageIndex > 0;

        /// <summary>
        /// A boolean indicating if there is a next page.
        /// </summary>
        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
