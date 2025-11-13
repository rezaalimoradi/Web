using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Application.Wishlists.Commands;
using CMS.Domain.Common;
using CMS.Domain.Wishlist.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Wishlists.CommandHandlers
{
    internal class DeleteWishlistItemCommandHandler : IAppRequestHandler<DeleteWishlistItemCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteWishlistItemCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(DeleteWishlistItemCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Wishlist>();

            var wishlist = await repo.GetAsync(
                predicate: w => w.CustomerId == request.CustomerIdentifier,
                include: w => w.Include(x => x.Items)
            );

            if (wishlist == null)
                return ResultModel<bool>.Success(false);

            // حذف آیتم
            wishlist.RemoveProduct(request.ProductId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // بررسی خالی بودن Wishlist
            if (!wishlist.Items.Any())
            {
                repo.Delete(wishlist);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return ResultModel<bool>.Success(true);
        }
    }

}
