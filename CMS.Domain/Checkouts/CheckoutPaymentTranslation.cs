using CMS.Domain.Common;

namespace CMS.Domain.Checkouts
{
    public class CheckoutPaymentTranslation : BaseEntity
    {
        protected CheckoutPaymentTranslation() { } // EF

        public CheckoutPaymentTranslation(
            long checkoutPaymentId,
            string language,
            string paymentMethodName,
            string failureMessage)
        {
            if (string.IsNullOrWhiteSpace(language))
                throw new Domain.Common.Exceptions.DomainException("Language is required.");
            if (string.IsNullOrWhiteSpace(paymentMethodName))
                throw new Domain.Common.Exceptions.DomainException("PaymentMethodName is required.");

            CheckoutPaymentId = checkoutPaymentId;
            Language = language.Trim().ToLowerInvariant();
            PaymentMethodName = paymentMethodName.Trim();
            FailureMessage = failureMessage?.Trim();
        }

        public long CheckoutPaymentId { get; private set; }
        public CheckoutPayment CheckoutPayment { get; private set; }

        /// <summary>
        /// Two-letter language code (e.g. "en", "fa")
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Localized payment method display name
        /// </summary>
        public string PaymentMethodName { get; private set; }

        /// <summary>
        /// Localized failure message if payment failed
        /// </summary>
        public string FailureMessage { get; private set; }

        // --- Behaviors ---
        public void UpdateTexts(string paymentMethodName, string failureMessage)
        {
            if (string.IsNullOrWhiteSpace(paymentMethodName))
                throw new Domain.Common.Exceptions.DomainException("PaymentMethodName is required.");

            PaymentMethodName = paymentMethodName.Trim();
            FailureMessage = failureMessage?.Trim();
        }
    }
}
