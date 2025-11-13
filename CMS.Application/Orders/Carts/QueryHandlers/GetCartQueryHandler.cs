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
    public class GetCartQueryHandler : IAppRequestHandler<GetCartQuery, ResultModel<CartDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetCartQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.GetRepository<Cart>()
                .GetAsync(
                    c => c.Id == request.Id && c.WebSiteId == _tenantContext.TenantId,
                    include: c => c.Include(cart => cart.Items) // فقط Items بدون Product
                );

            if (cart == null)
                return ResultModel<CartDto>.Fail("Cart not found.");

            var dto = new CartDto
            {
                Id = cart.Id,
                WebSiteId = cart.WebSiteId,
                CustomerIdentifier = cart.CustomerIdentifier,
                CreatedAt = cart.CreatedAt,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    ProductName = null // فعلاً ProductName پر نشده
                }).ToList()
            };

            return ResultModel<CartDto>.Success(dto);
        }

    }
}
