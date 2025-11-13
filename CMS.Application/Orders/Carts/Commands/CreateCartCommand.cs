using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using MediatR;

namespace CMS.Application.Orders.Carts.Commands
{
    public class CreateCartCommand : IAppRequest<ResultModel<long>>
    {
        public long WebSiteId { get; set; }
        public string CustomerIdentifier { get; set; }
    }
}
