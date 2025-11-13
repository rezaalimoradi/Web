using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Checkouts
{
    /// <summary>
    /// ترجمهٔ اطلاعات کلی checkout (مانند توضیحات، متد حمل‌ونقل، متد پرداخت، یادداشت سفارش).
    /// </summary>
    public class CheckoutTranslation : BaseEntity
    {
        protected CheckoutTranslation() { } // For EF

        public CheckoutTranslation(
            long checkoutId,
            long languageId,
            string orderNote,
            string shippingMethod,
            string paymentMethod)
        {
            if (checkoutId <= 0)
                throw new DomainException("CheckoutId is required.");
            if (languageId <= 0)
                throw new DomainException("LanguageId is required.");

            CheckoutId = checkoutId;
            WebSiteLanguageId = languageId;
            Update(orderNote, shippingMethod, paymentMethod);
        }

        public long CheckoutId { get; private set; }
        public Checkout Checkout { get; private set; }

        public long WebSiteLanguageId { get; private set; }

        public string OrderNote { get; private set; }
        public string ShippingMethod { get; private set; }
        public string PaymentMethod { get; private set; }

        // --- Behaviors ---
        public void Update(string orderNote, string shippingMethod, string paymentMethod)
        {
            OrderNote = orderNote?.Trim();
            ShippingMethod = shippingMethod?.Trim();
            PaymentMethod = paymentMethod?.Trim();
        }
    }
}
