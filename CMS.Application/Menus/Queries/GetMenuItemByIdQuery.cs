using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Menus.Queries
{
    public class GetMenuItemByIdQuery : IAppRequest<ResultModel<MenuItem?>>
    {
        public long Id { get; set; }
    }
}
