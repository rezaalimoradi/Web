using CMS.Application.Models.Common;
using CMS.Domain.Orders.Carts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Queries
{
    public class GetCartTranslationByIdQuery : IRequest<ResultModel<CartTranslation>>
    {
        public long Id { get; set; } // Translation Id
    }
}
