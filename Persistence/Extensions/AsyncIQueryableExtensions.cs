using CMS.Domain.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class AsyncIQueryableExtensions
    {
        /// <summary>
        /// Converts an <see cref="IQueryable{T}"/> to a paged list asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source query.</typeparam>
        /// <param name="source">The source query to paginate.</param>
        /// <param name="pageIndex">The zero-based index of the page to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="getOnlyTotalCount">
        /// If set to true, only the total count of items is returned without retrieving the actual data.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a paged list of items with metadata such as total count.
        /// </returns>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, bool getOnlyTotalCount = false)
        {
            if (source == null)
                return new PagedList<T>([], pageIndex, pageSize);

            pageSize = Math.Max(pageSize, 1);

            var count = await source.CountAsync();

            var data = new List<T>();

            if (!getOnlyTotalCount)
                data.AddRange(await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());

            return new PagedList<T>(data, pageIndex, pageSize, count);
        }
    }
}
