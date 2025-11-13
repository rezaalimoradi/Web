using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using MediatR;

namespace CMS.Application.Orders.Carts.Queries
{
    public class GetCartItemTranslationByIdQuery : IRequest<ResultModel<CartItemTranslationDto>>
    {
        public long Id { get; set; } // Translation Id
    }
}
