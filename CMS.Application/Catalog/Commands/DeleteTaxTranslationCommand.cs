using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class DeleteTaxTranslationCommand : IAppRequest<ResultModel<bool>>
    {
        public long Id { get; set; }
    }
}
