using CMS.Application.Interfaces.Messaging.Requests;

namespace CMS.Application.Tenants.Commands
{
    public class CreateLanguageCommand : IAppRequest<long>
    {
        public long WebsiteId { get; set; }

        public long LanguageId { get; set; }

        public bool IsDefault { get; set; }
    }
}
