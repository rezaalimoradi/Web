using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class CreateProductAttributeCommand : IAppRequest<ResultModel<long>>
    {
        public string Name { get; set; }
        public long WebSiteLanguageId { get; set; }

        public long? ProductId { get; set; }

        public bool AllowMultipleValues { get; set; } // چندمقداره یا تک‌مقداره
    }
}
