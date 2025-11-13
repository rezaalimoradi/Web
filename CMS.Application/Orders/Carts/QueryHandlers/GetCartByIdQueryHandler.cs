using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Application.Orders.Carts.Queries;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.QueryHandlers
{
    public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, ResultModel<CartDto>>
    {
        private readonly IRepository<Cart> _cartRepository;

        public GetCartByIdQueryHandler(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ResultModel<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.Table
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Translations)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (cart == null)
                return ResultModel<CartDto>.Fail("سبد خرید یافت نشد.");

            var dto = new CartDto
            {
                Id = cart.Id,
                CustomerIdentifier = cart.CustomerIdentifier,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Translations.FirstOrDefault()?.Name ?? "نامشخص",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount
                }).ToList(),
                TotalPrice = cart.Items.Sum(i => (i.UnitPrice - i.Discount) * i.Quantity)
            };

            return ResultModel<CartDto>.Success(dto);
        }
    }
}
