using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;
using System;

namespace CMS.Domain.Orders
{
    public class OrderAddressTranslation : BaseEntity
    {
        protected OrderAddressTranslation() { } // For EF

        public OrderAddressTranslation(
            long orderAddressId,
            long webSiteLanguageId,
            string contactName,
            string addressLine1,
            string addressLine2,
            string city)
        {
            OrderAddressId = orderAddressId;
            WebSiteLanguageId = webSiteLanguageId;

            Update(contactName, addressLine1, addressLine2, city);
        }

        // ---------------- Properties ----------------
        public long OrderAddressId { get; private set; }
        public OrderAddress OrderAddress { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        public string ContactName { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string City { get; private set; }

        // ---------------- Behaviors ----------------
        public void Update(
            string contactName,
            string addressLine1,
            string addressLine2,
            string city)
        {
            ContactName = SafeTrim(contactName, nameof(contactName));
            AddressLine1 = SafeTrim(addressLine1, nameof(addressLine1));
            AddressLine2 = SafeTrim(addressLine2); // optional
            City = SafeTrim(city, nameof(city));
        }

        // ---------------- Helpers ----------------
        private static string SafeTrim(string value, string paramName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (paramName != null)
                    throw new ArgumentNullException(paramName, $"{paramName} cannot be null or empty.");
                return null;
            }
            if (value.Length > 450)
                throw new ArgumentException($"{paramName ?? "Value"} cannot exceed 450 characters.", paramName);

            return value.Trim();
        }
    }
}
