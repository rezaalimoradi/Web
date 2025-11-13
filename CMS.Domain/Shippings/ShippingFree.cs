using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Shippings
{
    /// <summary>
    /// Represents free shipping rule.
    /// If order total >= MinimumOrderAmount then shipping fee is waived.
    /// </summary>
    public class ShippingFree : BaseEntity
    {
        protected ShippingFree() { } // EF

        public ShippingFree(decimal minimumOrderAmount)
        {
            if (minimumOrderAmount < 0)
                throw new DomainException("Minimum order amount must be non-negative.");

            MinimumOrderAmount = minimumOrderAmount;
            IsEnabled = true;
            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        public decimal MinimumOrderAmount { get; private set; }

        public bool IsEnabled { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        // --- Behaviors ---

        public void UpdateMinimumOrderAmount(decimal amount)
        {
            if (amount < 0)
                throw new DomainException("Minimum order amount must be non-negative.");

            MinimumOrderAmount = amount;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Enable()
        {
            IsEnabled = true;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Disable()
        {
            IsEnabled = false;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Checks if the rule applies for the given order total.
        /// </summary>
        public bool IsSatisfiedBy(decimal orderTotal) =>
            IsEnabled && orderTotal >= MinimumOrderAmount;
    }
}
