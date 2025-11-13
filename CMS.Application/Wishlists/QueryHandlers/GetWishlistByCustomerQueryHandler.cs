using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Media;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Application.Wishlists.Dtos;
using CMS.Application.Wishlists.Queries;
using CMS.Domain.Common;
using CMS.Domain.Wishlist.Entities;
using CMS.Domain.Media.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Wishlists.QueryHandlers
{
    internal class GetWishlistByCustomerQueryHandler : IAppRequestHandler<GetWishlistByCustomerQuery, ResultModel<WishlistDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly IMediaStorageStrategy _mediaStorageStrategy;

        public GetWishlistByCustomerQueryHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            IMediaStorageStrategy mediaStorageStrategy)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
            _mediaStorageStrategy = mediaStorageStrategy;
        }

        public async Task<ResultModel<WishlistDto>> Handle(GetWishlistByCustomerQuery request, CancellationToken cancellationToken)
        {
            var wishlist = await _unitOfWork.GetRepository<Wishlist>().GetAsync(
                predicate: w => w.CustomerId == request.CustomerIdentifier,
                include: w => w
                    .Include(x => x.Items)
                        .ThenInclude(i => i.Product)
                            .ThenInclude(p => p.Translations)
            );

            if (wishlist == null)
                return ResultModel<WishlistDto>.Success(new WishlistDto
                {
                    CustomerId = request.CustomerIdentifier,
                    Items = new List<WishlistItemDto>()
                });

            var mediaAttachRepo = _unitOfWork.GetRepository<MediaAttachment>();

            var itemsDto = new List<WishlistItemDto>();
            foreach (var item in wishlist.Items)
            {
                var product = item.Product;

                var translation = product.Translations
                    .FirstOrDefault(t => t.WebSiteLanguageId == _tenantContext.CurrentLanguageId)
                    ?? product.Translations.FirstOrDefault();

                // بارگذاری تصاویر از MediaAttachment
                var attachments = await mediaAttachRepo.GetAllAsync(
                    predicate: x => x.EntityId == product.Id &&
                                    x.EntityType == "Product" &&
                                    x.TenantId == _tenantContext.TenantId,
                    func: x => x.Include(a => a.MediaFile)
                );

                var imageUrls = attachments
                    .OrderByDescending(a => a.Purpose == "Main")
                    .Select(a => a.MediaFile != null
                        ? _mediaStorageStrategy.GetUrl(a.MediaFile.Key)
                        : string.Empty)
                    .Where(url => !string.IsNullOrEmpty(url))
                    .ToList();

                itemsDto.Add(new WishlistItemDto
                {
                    ProductId = product.Id,
                    ProductName = translation?.Name ?? string.Empty,
                    Price = product.Price,
                    ImageUrls = imageUrls,
                    InStock = product.StockQuantity > 0
                });
            }

            return ResultModel<WishlistDto>.Success(new WishlistDto
            {
                CustomerId = wishlist.CustomerId,
                Items = itemsDto
            });
        }
    }
}
