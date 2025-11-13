using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Menus.Commands
{
    public class CreateMenuItemTranslationCommand : IAppRequest<ResultModel<long>>
    {
        public long MenuId { get; set; }

        public string Title { get; private set; }
        public string Link { get; private set; }

        public long MenuItemId { get; private set; }
        public long WebSiteLanguageId { get; private set; }
    }
}
