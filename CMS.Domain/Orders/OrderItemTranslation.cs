using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;
using System;

namespace CMS.Domain.Orders
{
    public class OrderItemTranslation : BaseEntity
    {
        // ---------------- Properties ----------------
        public long OrderItemId { get; private set; }
        public string ProductName { get; private set; }
        public string? ProductDescription { get; private set; }
        public long WebSiteLanguageId { get; private set; }

        public WebSiteLanguage WebSiteLanguage { get; private set; }
        public OrderItem OrderItem { get; private set; }

        // ---------------- Constructors ----------------
        protected OrderItemTranslation() { } // For EF

        internal OrderItemTranslation(
            long orderItemId,
            long webSiteLanguageId,
            string productName,
            string? productDescription = null)
        {
            OrderItemId = ValidateId(orderItemId, nameof(orderItemId));
            WebSiteLanguageId = ValidateId(webSiteLanguageId, nameof(webSiteLanguageId));
            ProductName = ValidateName(productName, nameof(productName));
            ProductDescription = productDescription?.Trim();
        }

        // ---------------- Behaviors ----------------
        public void Update(string productName, string? productDescription = null)
        {
            ProductName = ValidateName(productName, nameof(productName));
            ProductDescription = productDescription?.Trim();
        }

        // ---------------- Private Helpers ----------------
        private static long ValidateId(long id, string paramName)
        {
            if (id <= 0) throw new DomainException($"{paramName} must be greater than zero.");
            return id;
        }

        private static string ValidateName(string name, string paramName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException($"{paramName} is required.");
            return name.Trim();
        }
    }
}
