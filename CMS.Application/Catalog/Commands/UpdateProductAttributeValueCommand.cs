using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class UpdateProductAttributeValueCommand : IAppRequest<ResultModel<bool>>
    {
        public long Id { get; set; }
        public long WebSiteId { get; set; }
        public long ProductId { get; set; }
        public long ProductAttributeId { get; set; }
        public long WebSiteLanguageId { get; set; }
        public string Value { get; set; }
    }
}
