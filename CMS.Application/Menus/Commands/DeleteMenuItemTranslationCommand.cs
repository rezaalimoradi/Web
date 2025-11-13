using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Menus.Commands
{
    public class DeleteMenuItemTranslationCommand : IAppRequest<ResultModel<long>>
    {
        public long MenuId { get; set; }
        public long MenuItemId { get; private set; }
        public long WebSiteLanguageId { get; private set; }
    }
}
