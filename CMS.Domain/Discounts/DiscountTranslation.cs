using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Discounts
{
    public class DiscountTranslation : BaseEntity
    {
        protected DiscountTranslation() { }

        public DiscountTranslation(long discountId, long languageId, string name, string description)
        {
            DiscountId = discountId;
            WebSiteLanguageId = languageId;
            Update(name, description);
        }

        public long DiscountId { get; private set; }
        public Discount Discount { get; private set; }

        public long WebSiteLanguageId { get; private set; }

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
