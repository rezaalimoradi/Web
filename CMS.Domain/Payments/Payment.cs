using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace CMS.Domain.Payments
{
    public class Payment : AggregateRoot
    {
        private readonly List<PaymentTranslation> _translations = new();
        public IReadOnlyCollection<PaymentTranslation> Translations => _translations.AsReadOnly();

        protected Payment() { } // For EF

        public Payment(long orderId, decimal amount, string paymentMethod)
        {
            if (orderId <= 0)
                throw new DomainException("OrderId is required.");
            if (amount <= 0)
                throw new DomainException("Payment amount must be greater than zero.");
            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new DomainException("Payment method is required.");

            OrderId = orderId;
            Amount = amount;
            PaymentMethod = paymentMethod.Trim();
            Status = PaymentStatus.Pending;

            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        public long OrderId { get; private set; }
        [JsonIgnore]
        public Order Order { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        public decimal Amount { get; private set; }
        public decimal PaymentFee { get; private set; }

        [StringLength(450)]
        public string PaymentMethod { get; private set; }

        [StringLength(450)]
        public string GatewayTransactionId { get; private set; }

        public PaymentStatus Status { get; private set; }

        public string FailureMessage { get; private set; }

        // --- Behaviors ---
        public void MarkAsSucceeded(string gatewayTransactionId)
        {
            Status = PaymentStatus.Succeeded;
            GatewayTransactionId = gatewayTransactionId?.Trim();
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void MarkAsFailed(string failureMessage)
        {
            Status = PaymentStatus.Failed;
            FailureMessage = failureMessage?.Trim();
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void ChangePaymentMethod(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
                throw new DomainException("Payment method is required.");
            PaymentMethod = method.Trim();
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void SetPaymentFee(decimal fee)
        {
            if (fee < 0)
                throw new DomainException("Fee cannot be negative.");
            PaymentFee = fee;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        // --- Translation behaviors ---
        public void AddTranslation(long languageId, string paymentMethod, string failureMessage)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new PaymentTranslation(Id, languageId, paymentMethod, failureMessage));
        }

        public void UpdateTranslation(long languageId, string paymentMethod, string failureMessage)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(paymentMethod, failureMessage);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }
    }
}
