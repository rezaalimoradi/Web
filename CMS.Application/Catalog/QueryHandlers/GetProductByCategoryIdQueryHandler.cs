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
    public class GetProductByCategoryIdQueryHandler
        : IAppRequestHandler<GetProductByCategoryIdQuery, ResultModel<IPagedList<Product>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductByCategoryIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        }

        public async Task<ResultModel<IPagedList<Product>>> Handle(
            GetProductByCategoryIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<Product>().GetAllPagedAsync(
                predicate: BuildPredicate(request),
                func: query => query
                    .Include(p => p.Translations)
                    .Include(p => p.Brand)
                        .ThenInclude(b => b!.Translations)
                    .Include(p => p.Product_ProductCategories)
                        .ThenInclude(m => m.ProductCategory)
                    .Include(p => p.MediaAttachments)
                        .ThenInclude(ma => ma.MediaFile),
                
                pageIndex: request.Page,
                pageSize: request.PageSize
            );

            return ResultModel<IPagedList<Product>>.Success(result);
        }

        // ──────────────────────────────────────────────────────────────
        // Predicate: فیلتر بر اساس تنانت، دسته‌بندی، قیمت، برند، نوع عطر
        // ──────────────────────────────────────────────────────────────
        private Expression<Func<Product, bool>> BuildPredicate(GetProductByCategoryIdQuery request)
        {
            return product =>
                product.WebSiteId == _tenantContext.TenantId &&
                (!request.CategoryId.HasValue ||
                    product.Product_ProductCategories.Any(m => m.ProductCategoryId == request.CategoryId.Value)) &&
                (!request.MinPrice.HasValue || product.Price >= request.MinPrice.Value) &&
                (!request.MaxPrice.HasValue || product.Price <= request.MaxPrice.Value) &&
                (string.IsNullOrWhiteSpace(request.Brand) ||
                    product.Brand != null &&
                    product.Brand.Translations.Any(t =>
                        t.Title != null &&
                        t.Title.Contains(request.Brand, StringComparison.OrdinalIgnoreCase))) &&
                (string.IsNullOrWhiteSpace(request.PerfumeType) ||
                    product.SKU == request.PerfumeType);
        }

        // ──────────────────────────────────────────────────────────────
        // OrderBy: مرتب‌سازی بر اساس نوع درخواست
        // ──────────────────────────────────────────────────────────────
        private static Expression<Func<IQueryable<Product>, IOrderedQueryable<Product>>> BuildOrderBy(string? sortBy)
        {
            return sortBy?.Trim().ToLowerInvariant() switch
            {
                "price-low" => q => q.OrderBy(p => p.Price),
                "price-high" => q => q.OrderByDescending(p => p.Price),
                "date" => q => q.OrderByDescending(p => p.Id),
                "popularity" => q => q.OrderByDescending(p => p.Id), // بعداً می‌تونی SoldCount اضافه کنی
                _ => q => q.OrderBy(p => p.Id)
            };
        }
    }
}