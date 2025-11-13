using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Orders
{
    public class OrderTranslation : BaseEntity
    {
        // ---------------- Properties ----------------
        public long OrderId { get; private set; }
        public long WebSiteLanguageId { get; private set; }

        public string? OrderNote { get; private set; }
        public string? ShippingMethod { get; private set; }
        public string? PaymentMethod { get; private set; }

        public WebSiteLanguage WebSiteLanguage { get; private set; }
        public Order Order { get; private set; }

        // ---------------- Constructors ----------------
        protected OrderTranslation() { } // For EF

        internal OrderTranslation(long orderId, long webSiteLanguageId, string orderNote, string shippingMethod, string paymentMethod)
        {
            OrderId = ValidateId(orderId, nameof(orderId));
            WebSiteLanguageId = ValidateId(webSiteLanguageId, nameof(webSiteLanguageId));
            OrderNote = TrimValue(orderNote);
            ShippingMethod = TrimValue(shippingMethod);
            PaymentMethod = TrimValue(paymentMethod);
        }

        // ---------------- Behaviors ----------------
        public void Update(string orderNote, string shippingMethod, string paymentMethod)
        {
            OrderNote = TrimValue(orderNote);
            ShippingMethod = TrimValue(shippingMethod);
            PaymentMethod = TrimValue(paymentMethod);
        }

        // ---------------- Private Helpers ----------------
        private static long ValidateId(long id, string paramName)
        {
            if (id <= 0) throw new DomainException($"{paramName} must be greater than zero.");
            return id;
        }

        private static string? TrimValue(string? value) => value?.Trim();
    }
}
