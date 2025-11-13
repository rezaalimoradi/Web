using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using MediatR;

namespace CMS.Application.Orders.Carts.Queries
{
    public class GetCartQuery : IRequest<ResultModel<CartDto>>
    {
        public long Id { get; set; }
    }
}
