using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Application.Wishlists.Commands;
using CMS.Domain.Common;
using CMS.Domain.Wishlist.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Wishlists.CommandHandlers
{
    internal class AddToWishlistCommandHandler : IAppRequestHandler<AddToWishlistCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public AddToWishlistCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(AddToWishlistCommand request, CancellationToken cancellationToken)
        {
            // 🧩 مرحله ۱: اعتبارسنجی ورودی‌ها
            if (request.CustomerIdentifier <= 0)
                return ResultModel<bool>.Fail("شناسه کاربر معتبر نیست یا کاربر وارد نشده است.");

            if (request.ProductId <= 0)
                return ResultModel<bool>.Fail("شناسه محصول معتبر نیست.");

            // 🧩 مرحله ۲: واکشی علاقه‌مندی کاربر (در همان Tenant)
            var repo = _unitOfWork.GetRepository<Wishlist>();

            var wishlist = await repo.Table
                .Include(w => w.Items)
                .FirstOrDefaultAsync(
                    w => w.CustomerId == request.CustomerIdentifier &&
                         w.WebsiteId == request.WebsiteId,
                    cancellationToken);

            // 🧩 مرحله ۳: ایجاد Wishlist در صورت نبود
            if (wishlist == null)
            {
                wishlist = new Wishlist(request.WebsiteId, request.CustomerIdentifier);
                await repo.InsertAsync(wishlist);
            }

            // 🧩 مرحله ۴: بررسی تکراری بودن محصول
            var alreadyExists = wishlist.Items.Any(i => i.ProductId == request.ProductId);
            if (alreadyExists)
            {
                return ResultModel<bool>.Fail("این محصول قبلاً در لیست علاقه‌مندی شما وجود دارد.");
            }

            // 🧩 مرحله ۵: افزودن و ذخیره
            wishlist.AddProduct(request.ProductId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel<bool>.Success(true);
        }
    }
}
