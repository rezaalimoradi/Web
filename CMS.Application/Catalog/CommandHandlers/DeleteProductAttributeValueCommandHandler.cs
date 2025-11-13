using CMS.Application.Catalog.Commands;
using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class DeleteProductAttributeValueCommandHandler
        : IAppRequestHandler<DeleteProductAttributeValueCommand, ResultModel<DeleteProductAttributeValueResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteProductAttributeValueCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<DeleteProductAttributeValueResultDto>> Handle(DeleteProductAttributeValueCommand request, CancellationToken cancellationToken)
        {
            if (request.ProductId <= 0 || request.AttributeId <= 0 || request.ValueId <= 0)
                return ResultModel<DeleteProductAttributeValueResultDto>.Fail("شناسه‌ها معتبر نیستند.");

            var productRepo = _unitOfWork.GetRepository<Product>();
            var product = await productRepo.GetAsync(p => p.Id == request.ProductId);
            if (product == null)
                return ResultModel<DeleteProductAttributeValueResultDto>.Fail("محصول یافت نشد.");

            if (product.WebSiteId != _tenantContext.TenantId)
                return ResultModel<DeleteProductAttributeValueResultDto>.Fail("شما مجاز به حذف مقدار این محصول نیستید.");

            var ppaRepo = _unitOfWork.GetRepository<ProductProductAttribute>();
            var ppa = await ppaRepo.GetAsync(
                predicate: x => x.ProductId == request.ProductId && x.ProductAttributeId == request.AttributeId,
                include: q => q.Include(x => x.ValueMappings)
            );

            if (ppa == null)
                return ResultModel<DeleteProductAttributeValueResultDto>.Fail("این ویژگی به محصول اختصاص داده نشده است.");

            var mappingRepo = _unitOfWork.GetRepository<ProductProductAttribute_ValueMapping>();
            var mapping = ppa.ValueMappings
                .FirstOrDefault(m => m.ProductAttributeValueId == request.ValueId);

            if (mapping == null)
                return ResultModel<DeleteProductAttributeValueResultDto>.Fail("مقدار مورد نظر یافت نشد.");

            mappingRepo.Delete(mapping);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var remainingMappings = await mappingRepo.GetAllAsync(
                predicate: m => m.ProductProductAttributeId == ppa.Id
            );

            var remainingValueIds = remainingMappings
                .Where(m => m.ProductAttributeValueId.HasValue)
                .Select(m => m.ProductAttributeValueId!.Value)
                .ToList();

            var dto = new DeleteProductAttributeValueResultDto
            {
                RemovedValueId = request.ValueId,
                RemainingValueIds = remainingValueIds
            };

            return ResultModel<DeleteProductAttributeValueResultDto>.Success(dto);
        }
    }
}
