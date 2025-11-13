using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class DeleteProductAttributeValueMappingCommandHandler
        : IAppRequestHandler<DeleteProductAttributeValueMappingCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteProductAttributeValueMappingCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(DeleteProductAttributeValueMappingCommand request, CancellationToken cancellationToken)
        {
            if (request.ProductId <= 0 || request.AttributeId <= 0 || request.ValueId <= 0)
                return ResultModel<bool>.Fail("شناسه‌های ورودی معتبر نیستند.");

            // 1. بررسی اینکه محصول وجود دارد و متعلق به tenant فعلی است
            var product = await _unitOfWork.GetRepository<Product>()
                .GetAsync(p => p.Id == request.ProductId);

            if (product == null)
                return ResultModel<bool>.Fail("محصول یافت نشد.");

            if (product.WebSiteId != _tenantContext.TenantId)
                return ResultModel<bool>.Fail("دسترسی به این محصول مجاز نیست.");

            // 2. پیدا کردن رابط محصول-ویژگی (PPA)
            var ppa = await _unitOfWork.GetRepository<ProductProductAttribute>()
                .GetAsync(pp => pp.ProductId == request.ProductId && pp.ProductAttributeId == request.AttributeId);

            if (ppa == null)
                return ResultModel<bool>.Fail("این ویژگی به محصول اختصاص داده نشده است.");

            // 3. پیدا کردن mapping مربوطه برای همان ppa و value
            var mapping = await _unitOfWork.GetRepository<ProductProductAttribute_ValueMapping>()
                .GetAsync(m => m.ProductProductAttributeId == ppa.Id && m.ProductAttributeValueId == request.ValueId);

            if (mapping == null)
                return ResultModel<bool>.Fail("رکورد مورد نظر جهت حذف یافت نشد.");

            // 4. حذف mapping
            _unitOfWork.GetRepository<ProductProductAttribute_ValueMapping>().Delete(mapping);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel<bool>.Success(true);
        }
    }
}
