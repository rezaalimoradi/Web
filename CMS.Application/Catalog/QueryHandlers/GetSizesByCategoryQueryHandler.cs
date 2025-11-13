using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CMS.Application.Catalog.QueryHandlers
{
    internal class GetSizesByCategoryQueryHandler
        : IAppRequestHandler<GetSizesByCategoryQuery, ResultModel<List<Product>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetSizesByCategoryQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<Product>>> Handle(GetSizesByCategoryQuery request, CancellationToken cancellationToken)
        {
            // دریافت محصولات مربوط به دسته‌بندی و Tenant فعلی
            var products = await _unitOfWork
                .GetRepository<Product>()
                .GetAllAsync(
                    predicate: p => p.Product_ProductCategories.FirstOrDefault().ProductCategoryId == request.CategoryId
                                    //&& !string.IsNullOrEmpty(p.Size)
                                    && p.WebSiteId == _tenantContext.TenantId
                );

            // استخراج سایزهای یکتا
            var distinctSizes = products
                //.Select(p => p.Size)
                .Distinct()
                .ToList();

            return ResultModel<List<Product>>.Success(distinctSizes);
        }
    }
}
