using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetProductCategoryByIdQuery : IAppRequest<ResultModel<ProductCategoryDto>>
    {
        public long Id { get; set; }
        public long LanguageId { get; set; }
    }
}
