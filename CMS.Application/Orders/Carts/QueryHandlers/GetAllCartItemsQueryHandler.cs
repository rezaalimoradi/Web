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

namespace CMS.Application.Orders.Carts.QueryHandlers
{
    public class GetAllCartItemsQueryHandler
        : IAppRequestHandler<GetAllCartItemsQuery, ResultModel<List<CartItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllCartItemsQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<CartItemDto>>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CartItem>();

            // بارگذاری CartItems مرتبط با Cart و Tenant
            var itemsQuery = repo.Table
                .Include(ci => ci.Translations)      // بارگذاری Translations
                .Include(ci => ci.Cart)             // بارگذاری Cart برای TenantId
                .Where(ci => ci.CartId == request.CartId && ci.Cart.WebSiteId == _tenantContext.TenantId)
                .AsQueryable();

            var items = await itemsQuery
                .Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    Discount = ci.Discount,
                    Translations = ci.Translations
                        .Select(t => new CartItemTranslationDto
                        {
                            Id = t.Id,
                            LanguageId = t.WebSiteLanguageId,
                            Title = t.Title
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken);

            return ResultModel<List<CartItemDto>>.Success(items);
        }
    }
}
