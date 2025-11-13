using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Queries
{
    public class GetProductPricesQuery : IAppRequest<ResultModel<List<ProductPriceDto>>>
    {
        public long ProductId { get; set; }

        public long LanguageId { get; set; }
    }
}
