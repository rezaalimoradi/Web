using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Discounts
{
    public class Discount : AggregateRoot
    {
        private readonly List<DiscountTranslation> _translations = new();
        public IReadOnlyCollection<DiscountTranslation> Translations => _translations.AsReadOnly();

        protected Discount() { } // For EF

        public Discount(string code, DiscountType type, decimal value, DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Discount code is required.");
            if (value <= 0)
                throw new DomainException("Discount value must be greater than zero.");

            Code = code.Trim();
            Type = type;
            Value = value;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = true;
        }

        public string Code { get; private set; }
        public DiscountType Type { get; private set; } // Percent, Fixed
        public decimal Value { get; private set; }
        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }
        public decimal? MinimumOrderAmount { get; private set; }
        public int? MaxUsageCount { get; private set; }
        public int UsageCount { get; private set; }
        public bool IsActive { get; private set; }

        // --- Behaviors ---
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void SetMinimumOrderAmount(decimal amount)
        {
            if (amount < 0)
                throw new DomainException("Minimum order amount cannot be negative.");
            MinimumOrderAmount = amount;
        }

        public void SetMaxUsage(int count)
        {
            if (count < 0)
                throw new DomainException("Max usage must be non-negative.");
            MaxUsageCount = count;
        }

        public decimal Apply(decimal orderTotal)
        {
            if (!IsValid(orderTotal))
                return 0;

            var discount = Type == DiscountType.Percent
                ? orderTotal * (Value / 100)
                : Value;

            return Math.Min(discount, orderTotal);
        }

        public bool IsValid(decimal orderTotal)
        {
            if (!IsActive)
                return false;
            if (StartDate.HasValue && StartDate.Value > DateTimeOffset.UtcNow)
                return false;
            if (EndDate.HasValue && EndDate.Value < DateTimeOffset.UtcNow)
                return false;
            if (MinimumOrderAmount.HasValue && orderTotal < MinimumOrderAmount.Value)
                return false;
            if (MaxUsageCount.HasValue && UsageCount >= MaxUsageCount.Value)
                return false;

            return true;
        }

        public void RegisterUsage()
        {
            UsageCount++;
        }

        // --- Translation behaviors ---
        public void AddTranslation(long languageId, string name, string description)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation already exists for this language.");

            _translations.Add(new DiscountTranslation(Id, languageId, name, description));
        }

        public void UpdateTranslation(long languageId, string name, string description)
        {
            var t = _translations.FirstOrDefault(x => x.WebSiteLanguageId == languageId);
            if (t == null)
                throw new DomainException("Translation not found.");

            t.Update(name, description);
        }

        public void RemoveTranslation(long languageId)
        {
            var t = _translations.FirstOrDefault(x => x.WebSiteLanguageId == languageId);
            if (t == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(t);
        }
    }
}
