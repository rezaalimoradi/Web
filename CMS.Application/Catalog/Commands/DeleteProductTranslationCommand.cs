using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class DeleteProductTranslationCommand : IAppRequest<ResultModel<long>>
    {
        public long ProductId { get; set; }
        public long LanguageId { get; set; }
    }
}
