using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Menus.Commands
{
    public class DeleteMenuItemCommand : IAppRequest<ResultModel<long>>
    {
        public long MenuItemId { get; set; }
        public long WebSiteLanguageId { get; set; }
    }
}
