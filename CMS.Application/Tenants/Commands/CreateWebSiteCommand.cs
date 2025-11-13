using CMS.Application.Interfaces.Messaging.Requests;

namespace CMS.Application.Tenants.Commands
{
    public class CreateWebSiteCommand : IAppRequest<long>
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string CompanyName { get; set; } = null!;

        public string? LogoUrl { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
