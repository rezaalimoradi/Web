using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using MediatR;

namespace CMS.Application.Orders.Carts.Queries
{
    public class GetAllCartItemTranslationsQuery : IRequest<ResultModel<List<CartItemTranslationDto>>>
    {
        public long CartItemId { get; set; }
    }
}
