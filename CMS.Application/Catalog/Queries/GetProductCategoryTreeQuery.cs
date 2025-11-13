using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetProductCategoryTreeQuery : IAppRequest<ResultModel<List<ProductCategoryTreeDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public long? CurrentLanguageId { get; set; }
    }
}
