using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Checkouts
{
    public class CheckoutShipping : BaseEntity
    {
        protected CheckoutShipping() { } // EF

        public CheckoutShipping(
            long checkoutId,
            string method,
            decimal amount)
        {
            if (string.IsNullOrWhiteSpace(method))
                throw new DomainException("Shipping method is required.");
            if (amount < 0)
                throw new DomainException("Shipping amount cannot be negative.");

            CheckoutId = checkoutId;
            Method = method.Trim();
            Amount = amount;
            IsEnabled = true;
        }

        public long CheckoutId { get; private set; }
        public Checkout Checkout { get; private set; }

        /// <summary>
        /// Technical shipping method key (e.g. "standard", "express")
        /// </summary>
        public string Method { get; private set; }

        public decimal Amount { get; private set; }

        public bool IsEnabled { get; private set; }

        // --- Behaviors ---
        public void Update(string method, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(method))
                throw new DomainException("Shipping method is required.");
            if (amount < 0)
                throw new DomainException("Shipping amount cannot be negative.");

            Method = method.Trim();
            Amount = amount;
        }

        public void Enable() => IsEnabled = true;
        public void Disable() => IsEnabled = false;
    }
}
