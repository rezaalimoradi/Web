using CMS.Application.CompareItems.Dtos;
using CMS.Application.CompareItems.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Media;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.CompareItems.Entities;
using CMS.Domain.Media.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.CompareItems.QueryHandlers
{
    internal class GetCompareByCustomerQueryHandler
        : IAppRequestHandler<GetCompareByCustomerQuery, ResultModel<CompareListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly IMediaStorageStrategy _mediaStorageStrategy;

        public GetCompareByCustomerQueryHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            IMediaStorageStrategy mediaStorageStrategy)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
            _mediaStorageStrategy = mediaStorageStrategy;
        }

        public async Task<ResultModel<CompareListDto>> Handle(GetCompareByCustomerQuery request, CancellationToken cancellationToken)
        {
            var compareRepo = _unitOfWork.GetRepository<CompareList>();
            var mediaAttachRepo = _unitOfWork.GetRepository<MediaAttachment>();

            var compareList = await compareRepo.Table
                .Include(x => x.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Translations)
                .Include(x => x.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Brand)
                            .ThenInclude(b => b.Translations)
                .FirstOrDefaultAsync(
                    x => x.CustomerId == request.CustomerId &&
                         x.WebsiteId == _tenantContext.TenantId,
                    cancellationToken);

            if (compareList == null || !compareList.Items.Any())
            {
                return ResultModel<CompareListDto>.Success(new CompareListDto
                {
                    CustomerId = request.CustomerId,
                    Items = new List<CompareItemsDto>()
                });
            }

            var itemsDto = new List<CompareItemsDto>();

            foreach (var item in compareList.Items)
            {
                var product = item.Product;

                if (product == null)
                    continue;

                // ترجمه بر اساس زبان فعلی
                var translation = product.Translations
                    .FirstOrDefault(t => t.WebSiteLanguageId == _tenantContext.CurrentLanguageId)
                    ?? product.Translations.FirstOrDefault();

                // 📸 گرفتن عکس‌ها از MediaAttachment
                var attachments = await mediaAttachRepo.GetAllAsync(
                    predicate: x => x.EntityId == product.Id &&
                                    x.EntityType == "Product" &&
                                    x.TenantId == _tenantContext.TenantId,
                    func: x => x.Include(a => a.MediaFile));

                // مرتب‌سازی عکس‌ها (عکس اصلی اول)
                var imageUrls = attachments
                    .OrderByDescending(a => a.Purpose == "Main")
                    .Select(a => a.MediaFile != null
                        ? _mediaStorageStrategy.GetUrl(a.MediaFile.Key)
                        : string.Empty)
                    .Where(url => !string.IsNullOrEmpty(url))
                    .ToList();

                // ساخت DTO برای هر محصول
                itemsDto.Add(new CompareItemsDto
                {
                    ProductId = product.Id,
                    ProductName = translation?.Name ?? "بدون نام",
                    Price = product.Price,
                    Brand = product.Brand?.Translations.FirstOrDefault()?.Title ?? "نامشخص",
                    InStock = product.StockQuantity > 0,
                    AddedAt = item.AddedAt,
                    ImageUrls = imageUrls
                });
            }

            var dto = new CompareListDto
            {
                CustomerId = compareList.CustomerId,
                Items = itemsDto
            };

            return ResultModel<CompareListDto>.Success(dto);
        }
    }
}
