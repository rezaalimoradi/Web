using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Checkouts
{
    public class CheckoutPayment : BaseEntity
    {
        protected CheckoutPayment() { } // EF

        public CheckoutPayment(long checkoutId, decimal amount, string paymentMethod, decimal paymentFee = 0)
        {
            if (checkoutId <= 0)
                throw new DomainException("CheckoutId is required.");
            if (amount < 0)
                throw new DomainException("Amount cannot be negative.");
            if (paymentFee < 0)
                throw new DomainException("Payment fee cannot be negative.");
            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new DomainException("PaymentMethod is required.");

            CheckoutId = checkoutId;
            Amount = amount;
            PaymentMethod = paymentMethod.Trim();
            PaymentFee = paymentFee;
            Status = CheckoutPaymentStatus.Pending;

            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        public long CheckoutId { get; private set; }
        public Checkout Checkout { get; private set; }

        public decimal Amount { get; private set; }
        public decimal PaymentFee { get; private set; }

        public string PaymentMethod { get; private set; }

        public string GatewayTransactionId { get; private set; }

        public CheckoutPaymentStatus Status { get; private set; }

        public string FailureMessage { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        // --- Behaviors ---
        public void MarkAsSucceeded(string gatewayTransactionId)
        {
            if (string.IsNullOrWhiteSpace(gatewayTransactionId))
                throw new DomainException("GatewayTransactionId is required.");

            Status = CheckoutPaymentStatus.Succeeded;
            GatewayTransactionId = gatewayTransactionId.Trim();
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void MarkAsFailed(string failureMessage)
        {
            if (string.IsNullOrWhiteSpace(failureMessage))
                throw new DomainException("FailureMessage is required.");

            Status = CheckoutPaymentStatus.Failed;
            FailureMessage = failureMessage.Trim();
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }
    }
}
