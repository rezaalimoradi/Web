using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Payments
{
    public class PaymentTranslation : BaseEntity
    {
        protected PaymentTranslation() { } // For EF

        public PaymentTranslation(long paymentId, long webSiteLanguageId, string paymentMethod, string failureMessage)
        {
            PaymentId = paymentId;
            WebSiteLanguageId = webSiteLanguageId;
            Update(paymentMethod, failureMessage);
        }

        public long PaymentId { get; private set; }
        public Payment Payment { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        public string PaymentMethod { get; private set; }
        public string FailureMessage { get; private set; }

        public void Update(string paymentMethod, string failureMessage)
        {
            PaymentMethod = paymentMethod?.Trim();
            FailureMessage = failureMessage?.Trim();
        }
    }
}
