using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class CreateProductRelationCommandHandler
        : IAppRequestHandler<CreateProductRelationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateProductRelationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateProductRelationCommand request, CancellationToken cancellationToken)
        {
            if (request.RelatedProductIds == null || !request.RelatedProductIds.Any())
                return ResultModel<long>.Fail("حداقل یک محصول مرتبط باید انتخاب شود.");

            var relationRepo = _unitOfWork.GetRepository<ProductRelation>();
            var productRepo = _unitOfWork.GetRepository<Product>();

            var mainProductExists = await productRepo.Table
                .AnyAsync(p => p.Id == request.ProductId && p.WebSiteId == _tenantContext.TenantId, cancellationToken);

            if (!mainProductExists)
                return ResultModel<long>.Fail("محصول اصلی یافت نشد یا به این وب‌سایت تعلق ندارد.");

            foreach (var relatedId in request.RelatedProductIds.Distinct())
            {
                var relatedProductExists = await productRepo.Table
                    .AnyAsync(p => p.Id == relatedId && p.WebSiteId == _tenantContext.TenantId, cancellationToken);

                if (!relatedProductExists)
                    continue;

                bool exists = await relationRepo.Table
                    .AnyAsync(x => x.ProductId == request.ProductId && x.RelatedProductId == relatedId, cancellationToken);

                if (exists)
                    continue;

                var relation = new ProductRelation(request.ProductId, relatedId);
                await relationRepo.InsertAsync(relation);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel<long>.Success(request.ProductId);
        }
    }
}
