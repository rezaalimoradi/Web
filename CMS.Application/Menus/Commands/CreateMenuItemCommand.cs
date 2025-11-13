using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Menus.Commands
{
    public class CreateMenuItemCommand : IAppRequest<ResultModel<long>>
    {
        public long MenuId { get; private set; }
        public long? ParentId { get; set; }

        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }

        public string Title { get; private set; }
        public string Link { get; private set; }

        public long WebSiteLanguageId { get; private set; }
    }
}
