using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using MediatR;

namespace CMS.Application.Orders.Carts.Queries
{
    public class GetAllCartsQuery : IAppRequest<ResultModel<List<CartDto>>>
    {
    }
}