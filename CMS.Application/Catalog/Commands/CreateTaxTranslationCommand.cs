using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class CreateTaxTranslationCommand : IAppRequest<ResultModel<TaxTranslationDto>>
    {
        public long TaxId { get; set; }
        public long LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
