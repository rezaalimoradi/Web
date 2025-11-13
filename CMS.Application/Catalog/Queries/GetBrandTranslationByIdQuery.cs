using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetBrandTranslationByIdQuery : IAppRequest<ResultModel<BrandTranslationDto>>
    {
        public long Id { get; set; }
    }
}
