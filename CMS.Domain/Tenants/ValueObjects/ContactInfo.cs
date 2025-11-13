namespace CMS.Domain.Tenants.ValueObjects
{
    /// <summary>
    /// Represents contact information for a website or tenant.
    /// </summary>
    public class ContactInfo
    {
        /// <summary>
        /// Gets the email address for the contact.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the phone number for the contact.
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Gets the physical address for the contact.
        /// </summary>
        public string Address { get; private set; }

        // Default constructor for EF Core or serialization
        private ContactInfo() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInfo"/> class.
        /// </summary>
        /// <param name="email">The email address for the contact.</param>
        /// <param name="phoneNumber">The phone number for the contact.</param>
        /// <param name="address">The physical address for the contact.</param>
        public ContactInfo(string email, string phoneNumber, string address)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
