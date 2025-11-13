using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Menus.Queries
{
    public class GetMenuItemTranslationByIdQuery : IAppRequest<ResultModel<MenuItemTranslation?>>
    {
        public long Id { get; set; }
    }
}
