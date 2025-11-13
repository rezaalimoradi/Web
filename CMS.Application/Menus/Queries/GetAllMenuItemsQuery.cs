using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Menus.Queries
{
    public class GetAllMenuItemsQuery : IAppRequest<ResultModel<IPagedList<MenuItem>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
