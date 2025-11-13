using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Orders.Dtos;

public class GetOrderByIdQuery : IAppRequest<ResultModel<OrderDto>>
{
    public long Id { get; }
    public string CustomerIdentifier { get; }

    public GetOrderByIdQuery(long id, string customerIdentifier)
    {
        Id = id;
        CustomerIdentifier = customerIdentifier;
    }
}
