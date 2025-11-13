using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Queries
{

    public class GetRelatedProductsByProductIdQuery : IAppRequest<ResultModel<List<ProductDto>>>
    {
        public long ProductId { get; set; }
    }
}
