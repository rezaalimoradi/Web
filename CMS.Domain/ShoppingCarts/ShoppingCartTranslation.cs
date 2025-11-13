using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis; // اینو هم اضافه کن

namespace CMS.Domain.ShoppingCarts
{
    public class ShoppingCartTranslation : BaseEntity
    {
        protected ShoppingCartTranslation() { } // EF

        public ShoppingCartTranslation(long cartId, long languageId, string note)
        {
            if (cartId <= 0)
                throw new DomainException("CartId is required.");
            if (languageId <= 0)
                throw new DomainException("LanguageId is required.");

            ShoppingCartId = cartId;
            WebSiteLanguageId = languageId;
            Note = note?.Trim();
        }

        public long ShoppingCartId { get; private set; }
        public ShoppingCart ShoppingCart { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; } // 👈 اینو اضافه کن

        /// <summary>
        /// یادداشت یا توضیح چندزبانه برای سبد خرید
        /// </summary>
        public string Note { get; private set; }

        public void Update(string note)
        {
            Note = note?.Trim();
        }
    }
}
