using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Application.Orders.Carts.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.QueryHandlers
{
    public class GetCartItemByIdQueryHandler : IAppRequestHandler<GetCartItemByIdQuery, ResultModel<CartItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetCartItemByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<CartItemDto>> Handle(GetCartItemByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CartItem>();

            // فقط از فیلدهای موجود در CartItem استفاده می‌کنیم
            var item = await repo.Table
                .Where(ci => ci.Id == request.Id && ci.CartId > 0) // شرط WebSiteId فعلاً حذف شد
                .Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    Discount = ci.Discount,
                    ProductName = null, // اگر Product mapping شد می‌توان این را پر کرد
                    CartId = ci.CartId,
                    Translations = ci.Translations.Select(t => new CartItemTranslationDto
                    {
                        Id = t.Id,
                        LanguageId = t.WebSiteLanguageId,
                        Title = t.Title
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (item == null)
                return ResultModel<CartItemDto>.Fail("Cart item not found");

            return ResultModel<CartItemDto>.Success(item);
        }

    }
}
