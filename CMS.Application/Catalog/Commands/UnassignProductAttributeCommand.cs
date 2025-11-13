using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class UnassignProductAttributeCommand : IAppRequest<ResultModel<bool>>
    {
        public long ProductId { get; set; }
        public long AttributeId { get; set; }
    }
}
