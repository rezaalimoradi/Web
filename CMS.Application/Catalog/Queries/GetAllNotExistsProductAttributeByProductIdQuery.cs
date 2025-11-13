using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetAllNotExistsProductAttributeByProductIdQuery : IAppRequest<ResultModel<IPagedList<ProductAttribute>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public long ProductId { get; set; }
    }
}
