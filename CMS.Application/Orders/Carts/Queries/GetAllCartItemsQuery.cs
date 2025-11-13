using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using MediatR;

namespace CMS.Application.Orders.Carts.Queries
{
    public class GetAllCartItemsQuery : IRequest<ResultModel<List<CartItemDto>>>
    {
        public long CartId { get; set; }
    }
}
