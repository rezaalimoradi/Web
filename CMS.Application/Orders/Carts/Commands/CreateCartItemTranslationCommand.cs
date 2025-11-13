using CMS.Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Commands
{
    public class CreateCartItemTranslationCommand : IRequest<ResultModel<long>>
    {
        public long CartId { get; set; }        // برای چک tenancy
        public long CartItemId { get; set; }
        public long LanguageId { get; set; }
        public string Title { get; set; }
    }
}
