using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Application.Orders.Carts.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetCartByCustomerQueryHandler
    : IAppRequestHandler<GetCartByCustomerQuery, ResultModel<CartDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantContext _tenantContext;

    public GetCartByCustomerQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
    {
        _unitOfWork = unitOfWork;
        _tenantContext = tenantContext;
    }

    public async Task<ResultModel<CartDto>> Handle(GetCartByCustomerQuery request, CancellationToken cancellationToken)
    {
        var tenantId = _tenantContext.TenantId;

        // تعیین شناسه کاربر
        var userIdentifier = _tenantContext.UserId?.ToString();
        var guestIdentifier = _tenantContext.CustomerIdentifier ?? "guest-" + Guid.NewGuid().ToString();
        var currentIdentifier = userIdentifier ?? guestIdentifier;

        var repo = _unitOfWork.GetRepository<Cart>();

        // 🟩 سعی می‌کنیم Cart کاربر یا Guest رو پیدا کنیم
        var cartEntity = await repo.Table
            .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                    .ThenInclude(p => p.Translations)
            .Include(c => c.Translations)
            .FirstOrDefaultAsync(c => c.WebSiteId == tenantId &&
                                      c.CustomerIdentifier == currentIdentifier,
                               cancellationToken);

        // اگر کاربر لاگین شده و Cart پیدا نشد، ولی Guest Cart داریم، اون رو به کاربر منتقل کنیم
        if (cartEntity == null && userIdentifier != null)
        {
            var guestCart = await repo.Table
                .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Translations)
                .Include(c => c.Translations)
                .FirstOrDefaultAsync(c => c.WebSiteId == tenantId &&
                                          c.CustomerIdentifier == guestIdentifier,
                                   cancellationToken);

            if (guestCart != null)
            {
                guestCart.MergeWithUser(userIdentifier);
                cartEntity = guestCart;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        // اگر باز هم Cart پیدا نشد، یک Cart خالی برگردان
        if (cartEntity == null)
            return ResultModel<CartDto>.Success(new CartDto
            {
                Id = 0,
                CustomerIdentifier = currentIdentifier,
                Items = new List<CartItemDto>(),
                Translations = new List<CartTranslationDto>(),
                TotalPrice = 0
            });

        // Mapping به DTO
        var cartDto = new CartDto
        {
            Id = cartEntity.Id,
            CustomerIdentifier = cartEntity.CustomerIdentifier,
            Items = cartEntity.Items.Select(i => new CartItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                ProductName = i.Product?.Translations.FirstOrDefault()?.Name ?? "بدون نام محصول"
            }).ToList(),
            Translations = cartEntity.Translations.Select(t => new CartTranslationDto
            {
                Id = t.Id,
                LanguageId = t.WebSiteLanguageId,
                Title = t.Title,
                Description = t.Description
            }).ToList()
        };

        // جمع کل قیمت
        cartDto.TotalPrice = cartDto.Items.Sum(i => (i.UnitPrice - i.Discount) * i.Quantity);

        return ResultModel<CartDto>.Success(cartDto);
    }
}
