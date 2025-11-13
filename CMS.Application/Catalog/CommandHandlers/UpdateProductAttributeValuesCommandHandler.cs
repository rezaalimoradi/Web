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
    public class UpdateProductAttributeValuesCommandHandler
        : IAppRequestHandler<UpdateProductAttributeValuesCommand, ResultModel<UpdateProductAttributeValuesResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateProductAttributeValuesCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<UpdateProductAttributeValuesResultDto>> Handle(UpdateProductAttributeValuesCommand request, CancellationToken cancellationToken)
        {
            if (request.ProductId <= 0 || request.AttributeId <= 0)
                return ResultModel<UpdateProductAttributeValuesResultDto>.Fail("شناسه محصول یا ویژگی معتبر نیست.");

            var productRepo = _unitOfWork.GetRepository<Product>();
            var ppaRepo = _unitOfWork.GetRepository<ProductProductAttribute>();
            var mappingRepo = _unitOfWork.GetRepository<ProductProductAttribute_ValueMapping>();

            // ✅ 1) بررسی اینکه محصول متعلق به Tenant جاری هست یا نه
            var product = await productRepo.GetAsync(p => p.Id == request.ProductId);
            if (product == null || product.WebSiteId != _tenantContext.TenantId)
                return ResultModel<UpdateProductAttributeValuesResultDto>.Fail("دسترسی به این محصول مجاز نیست.");

            // ✅ 2) پیدا کردن (یا ساختن) PPA مخصوص همین محصول و ویژگی
            var ppa = await ppaRepo.GetAsync(x =>
                x.ProductId == request.ProductId &&
                x.ProductAttributeId == request.AttributeId);

            if (ppa == null)
            {
                ppa = new ProductProductAttribute(request.ProductId, request.AttributeId);
                await ppaRepo.InsertAsync(ppa);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // دوباره بخوان برای داشتن ID معتبر
                ppa = await ppaRepo.GetAsync(x => x.Id == ppa.Id);
            }

            // ✅ 3) گرفتن AllowMultipleValues از ویژگی
            var attr = await _unitOfWork.GetRepository<ProductAttribute>()
                .GetAsync(a => a.Id == request.AttributeId);

            var allowMultiple = attr?.AllowMultipleValues ?? false;

            // ✅ 4) آماده‌سازی مقادیر ورودی
            var newValues = (request.ValueIds ?? new List<long>())
                .Where(x => x > 0)
                .Distinct()
                .ToList();

            if (!newValues.Any())
                return ResultModel<UpdateProductAttributeValuesResultDto>.Fail("هیچ مقداری برای ویژگی ارسال نشده است.");

            if (!allowMultiple && newValues.Count > 1)
                return ResultModel<UpdateProductAttributeValuesResultDto>.Fail("این ویژگی تنها یک مقدار قابل انتخاب دارد.");

            // ✅ 5) تمام mappingهای فعلی را بخوان
            var existingMappings = await mappingRepo.GetAllAsync(m => m.ProductProductAttributeId == ppa.Id);
            var existingValueIds = existingMappings
                .Where(m => m.ProductAttributeValueId.HasValue)
                .Select(m => m.ProductAttributeValueId!.Value)
                .ToHashSet();

            // ✅ 6) محاسبه تغییرات
            var toAdd = newValues.Except(existingValueIds).ToList();
            var toRemove = existingValueIds.Except(newValues).ToList();
            var already = existingValueIds.Intersect(newValues).ToList();

            // ✅ 7) حذف فقط آن‌هایی که در لیست جدید نیستند
            foreach (var m in existingMappings.Where(m => m.ProductAttributeValueId.HasValue && toRemove.Contains(m.ProductAttributeValueId.Value)))
                mappingRepo.Delete(m);

            // ✅ 8) اضافه کردن فقط موارد جدید
            foreach (var vid in toAdd)
            {
                var mapping = new ProductProductAttribute_ValueMapping(ppa.Id, vid);
                await mappingRepo.InsertAsync(mapping);
            }

            // ✅ 9) ذخیره در دیتابیس
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // ✅ 10) ساخت نتیجه خروجی
            var dto = new UpdateProductAttributeValuesResultDto
            {
                AddedIds = toAdd,
                RemovedIds = toRemove,
                AlreadyExistsIds = already
            };

            return ResultModel<UpdateProductAttributeValuesResultDto>.Success(dto);
        }
    }
}
