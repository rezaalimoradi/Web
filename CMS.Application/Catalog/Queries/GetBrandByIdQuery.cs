using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetBrandByIdQuery : IAppRequest<ResultModel<BrandDto>>
    {
        public long Id { get; set; }
        public long LanguageId { get; set; }
    }
}
