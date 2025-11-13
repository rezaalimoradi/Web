using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Pricing
{
    public class CatalogRuleTranslation : BaseEntity
    {
        protected CatalogRuleTranslation() { } // For EF

        public CatalogRuleTranslation(long catalogRuleId, string culture, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(culture))
                throw new DomainException("Culture is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required.");

            CatalogRuleId = catalogRuleId;
            Culture = culture;
            Name = name.Trim();
            Description = description?.Trim();
        }

        public long CatalogRuleId { get; private set; }
        public CatalogRule CatalogRule { get; private set; }

        public string Culture { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required.");

            Name = name.Trim();
            Description = description?.Trim();
        }
    }
}
