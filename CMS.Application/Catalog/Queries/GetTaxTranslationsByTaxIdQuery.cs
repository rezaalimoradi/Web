using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Queries
{
    public class GetTaxTranslationsByTaxIdQuery : IAppRequest<ResultModel<List<TaxTranslationDto>>>
    {
        public long TaxId { get; set; }
    }
}
