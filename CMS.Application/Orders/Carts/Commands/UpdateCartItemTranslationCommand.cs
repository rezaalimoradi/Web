using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Commands
{
    public class UpdateCartItemTranslationCommand : IAppRequest<ResultModel<CartUpdateResultDto>>
    {
        public long CartId { get; set; }        // برای چک tenancy
        public long CartItemId { get; set; }
        public long LanguageId { get; set; }
        public string Title { get; set; }
    }
}
