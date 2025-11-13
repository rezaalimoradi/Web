using CMS.Application.Interfaces.Messaging.Requests;

namespace CMS.Application.Tenants.Queries
{
    public class GetDefaultLanguageCodeQuery : IAppRequest<string>
    {
        public long WebSiteId { get; set; }
    }
}
