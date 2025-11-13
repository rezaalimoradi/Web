using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetProductCategoryMappingsQuery : IAppRequest<ResultModel<IPagedList<Product_ProductCategory_Mapping>>>
    {
        public long ProductId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
