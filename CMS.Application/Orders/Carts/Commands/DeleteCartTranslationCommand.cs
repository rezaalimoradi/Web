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
    public class DeleteCartTranslationCommand : IAppRequest<ResultModel<bool>>
    {
        public long CartId { get; set; }
        public long LanguageId { get; set; }
    }
}
