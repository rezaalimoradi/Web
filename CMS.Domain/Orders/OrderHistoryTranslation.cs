using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.OrderHistories;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Orders
{
    public class OrderHistoryTranslation : BaseEntity
    {
        // ---------------- Properties ----------------
        public long OrderHistoryId { get; private set; }
        public string Note { get; private set; }
        public string OrderSnapshot { get; private set; }
        public long WebSiteLanguageId { get; private set; }

        public WebSiteLanguage WebSiteLanguage { get; private set; }
        public OrderHistory OrderHistory { get; private set; }

        // ---------------- Constructors ----------------
        protected OrderHistoryTranslation() { } // EF Core

        internal OrderHistoryTranslation(
            long orderHistoryId,
            long webSiteLanguageId,
            string note,
            string orderSnapshot)
        {
            if (orderHistoryId <= 0)
                throw new DomainException("OrderHistoryId is required.");

            OrderHistoryId = orderHistoryId;
            WebSiteLanguageId = webSiteLanguageId;
            Note = note?.Trim();
            OrderSnapshot = orderSnapshot?.Trim()
                ?? throw new ArgumentNullException(nameof(orderSnapshot));
        }

        // ---------------- Behaviors ----------------
        public void Update(string note, string orderSnapshot)
        {
            Note = note?.Trim();
            OrderSnapshot = orderSnapshot?.Trim()
                ?? throw new ArgumentNullException(nameof(orderSnapshot));
        }
    }
}
