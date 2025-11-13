using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Queries
{
    public class GetSizesByCategoryQuery : IAppRequest<ResultModel<List<Product>>>
    {
        public long CategoryId { get; set; }
    }
}
