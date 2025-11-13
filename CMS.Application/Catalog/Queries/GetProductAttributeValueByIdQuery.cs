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
    public record GetProductAttributeValueByIdQuery(long Id)
        : IAppRequest<ResultModel<ProductAttributeValue>>;
}
