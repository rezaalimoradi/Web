using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Application.Tenants.Queries
{
    public class GetWebSitesPagedQuery : IAppRequest<IPagedList<WebSite>>
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SearchQuery { get; set; } = string.Empty;
    }
}
