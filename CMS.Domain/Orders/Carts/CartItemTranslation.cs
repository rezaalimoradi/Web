using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;
using System;

namespace CMS.Domain.Orders.Carts
{
    public class CartItemTranslation : BaseEntity
    {
        protected CartItemTranslation() { }

        public long CartItemId { get; private set; }
        public long WebSiteLanguageId { get; private set; }
        public string Title { get; private set; }

        // Navigation properties
        public CartItem CartItem { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        internal CartItemTranslation(long cartItemId, long languageId, string title)
        {
            CartItemId = cartItemId;
            WebSiteLanguageId = languageId;
            SetTitle(title);
        }

        public void Update(string title) => SetTitle(title);

        private void SetTitle(string title)
        {
            ValidateTitle(title);
            Title = title.Trim();
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title is required");
            if (title.Length > 250)
                throw new DomainException("Title cannot exceed 250 characters");
        }
    }
}
