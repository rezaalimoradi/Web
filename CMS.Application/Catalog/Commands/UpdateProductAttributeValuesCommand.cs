using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Commands
{
    public class UpdateProductAttributeValuesCommand : IAppRequest<ResultModel<UpdateProductAttributeValuesResultDto>>
    {
        public long ProductId { get; set; }
        public long AttributeId { get; set; }
        public List<long> ValueIds { get; set; } = new();
    }
}
