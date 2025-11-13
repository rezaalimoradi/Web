using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class UpdateProductTranslationCommand : IAppRequest<ResultModel<long>>
    {
        public long ProductId { get; set; }
        public long LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
}
