using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using MediatR;

namespace CMS.Application.Orders.Carts.Queries
{
    public class GetCartByIdQuery : IAppRequest<ResultModel<CartDto>>
    {
        public long Id { get; }

        public GetCartByIdQuery(long id)
        {
            Id = id;
        }
    }
}
