using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class UpdateProductAttributeCommand : IAppRequest<ResultModel<bool>>
    {
        public long AttributeId { get; set; }
        public long WebSiteLanguageId { get; set; }
        public string Name { get; set; }
    }
}
