using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class DeleteProductAttributeValueCommand : IAppRequest<ResultModel<DeleteProductAttributeValueResultDto>>
    {
        public long ProductId { get; set; }
        public long AttributeId { get; set; }
        public long ValueId { get; set; }
    }
}
