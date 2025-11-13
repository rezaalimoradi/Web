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
    public class CreateCartItemCommand : IAppRequest<ResultModel<long>>
    {
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}
