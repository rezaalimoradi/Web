using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.ShoppingCarts
{
    public class ShoppingCartItemTranslation : BaseEntity
    {
        protected ShoppingCartItemTranslation() { } // EF

        public ShoppingCartItemTranslation(long itemId, long languageId, string name, string description = null)
        {
            if (itemId <= 0)
                throw new DomainException("ItemId is required.");
            if (languageId <= 0)
                throw new DomainException("LanguageId is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required.");

            ShoppingCartItemId = itemId;
            WebSiteLanguageId = languageId;
            Name = name.Trim();
            Description = description?.Trim();
        }

        public long ShoppingCartItemId { get; private set; }
        public ShoppingCartItem ShoppingCartItem { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

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
