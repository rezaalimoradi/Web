using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Dtos;

namespace CMS.Application.Tenants.Queries
{
    public class GetWebSiteByDomainQuery : IAppRequest<WebSiteDto>
    {
        public string Domain { get; set; }

        public GetWebSiteByDomainQuery(string domain)
        {
            Domain = domain;
        }
    }
}
