using Application.Common;

namespace CMS.Application.Users.Commands
{
    public class UpdateUserProfileCommand : IAppRequest<bool>
    {
        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? NewPassword { get; set; }

        public AddressCommandModel? BillingAddress { get; set; }
        public AddressCommandModel? ShippingAddress { get; set; }
    }

    public class AddressCommandModel
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
    }
}
