using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class CreateProductAttributeValueCommand : IAppRequest<ResultModel<long>>
    {
        public long ProductAttributeId { get; set; }
        public string Value { get; set; }
        public long WebSiteLanguageId { get; set; }
    }
}
