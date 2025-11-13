using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Dtos;

namespace CMS.Application.Tenants.Queries
{
    public class GetWebSiteLanguagesQuery : IAppRequest<List<WebSiteLanguageDto>>
    {
        public long WebSiteId { get; set; }
    }
}
