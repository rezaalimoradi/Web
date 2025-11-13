using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Pricing
{
    public class CartRuleTranslation : BaseEntity
    {
        protected CartRuleTranslation() { } // EF

        public CartRuleTranslation(long cartRuleId, string culture, string name, string description = null)
        {
            if (cartRuleId <= 0)
                throw new DomainException("CartRuleId is required.");
            if (string.IsNullOrWhiteSpace(culture))
                throw new DomainException("Culture is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required.");

            CartRuleId = cartRuleId;
            Culture = culture.Trim().ToLowerInvariant();
            Name = name.Trim();
            Description = description?.Trim();
        }

        public long CartRuleId { get; private set; }
        public CartRule CartRule { get; private set; }

        /// <summary>
        /// Culture code like "en-us", "fa-ir"
        /// </summary>
        public string Culture { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public void Update(string name, string description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required.");

            Name = name.Trim();
            Description = description?.Trim();
        }
    }
}
