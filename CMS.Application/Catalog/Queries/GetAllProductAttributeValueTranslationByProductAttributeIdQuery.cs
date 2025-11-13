using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetAllProductAttributeValueTranslationByProductAttributeIdQuery : IAppRequest<ResultModel<IPagedList<ProductAttributeValue>>>
    {
        public long ProductAttributeId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
