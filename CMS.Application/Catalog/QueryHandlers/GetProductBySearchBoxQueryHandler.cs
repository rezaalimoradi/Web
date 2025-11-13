using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetProductBySearchBoxQueryHandler
        : IAppRequestHandler<GetProductBySearchBoxQuery, ResultModel<IPagedList<Product>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductBySearchBoxQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<Product>>> Handle(
            GetProductBySearchBoxQuery request,
            CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Product>();

            Expression<Func<Product, bool>> predicate = x =>
                x.WebSiteId == _tenantContext.TenantId &&
                (!request.CategoryId.HasValue || x.Product_ProductCategories.FirstOrDefault().ProductCategoryId == request.CategoryId.Value);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                predicate = AndAlso(predicate, x =>
                    x.Translations.Any(t => t.Name.Contains(request.SearchTerm)));
            }

            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> includes =
                q => q.Include(y => y.Translations);

            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy =
                q => q.OrderBy(p => p.Id);

            var result = await repository.GetAllPagedAsync(
                predicate: predicate,
                func: q => orderBy(includes(q)),
                pageIndex: request.Page,
                pageSize: request.PageSize
            );

            return ResultModel<IPagedList<Product>>.Success(result);
        }


        private static Expression<Func<T, bool>> AndAlso<T>(
            Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var combined = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    Expression.Invoke(expr1, parameter),
                    Expression.Invoke(expr2, parameter)
                ),
                parameter
            );
            return combined;
        }
    }
}
