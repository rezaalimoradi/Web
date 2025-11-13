using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetProductCategoriesByLanguageQuery : IAppRequest<ResultModel<List<ProductCategoryDto>>>
    {
        public long LanguageId { get; set; }
    }
}
