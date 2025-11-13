using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Commands
{
    public class UpdateCartCommand : IAppRequest<ResultModel<bool>>
    {
        public long CartId { get; set; }
        public string CustomerIdentifier { get; set; } = "";
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
