using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetBrandsByCategoryQuery : IAppRequest<ResultModel<List<BrandDto>>>
    {
        public long CategoryId { get; set; }
    }
}
