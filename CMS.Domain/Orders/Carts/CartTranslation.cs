using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;
using System;

namespace CMS.Domain.Orders.Carts
{
    public class CartTranslation : BaseEntity
    {
        protected CartTranslation() { }

        public long CartId { get; private set; }
        public long WebSiteLanguageId { get; private set; }

        public string Title { get; private set; }
        public string? Description { get; private set; }

        // Navigation properties
        public Cart Cart { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        internal CartTranslation(long cartId, long languageId, string title, string? description)
        {
            CartId = cartId;
            WebSiteLanguageId = languageId;
            SetValues(title, description);
        }

        public void Update(string title, string? description) => SetValues(title, description);

        private void SetValues(string title, string? description)
        {
            ValidateTitle(title);
            Title = title.Trim();
            Description = description?.Trim();
            ValidateDescriptionLength(Description);
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title is required");
            if (title.Length > 250)
                throw new DomainException("Title cannot exceed 250 characters");
        }

        private void ValidateDescriptionLength(string? description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 1000)
                throw new DomainException("Description cannot exceed 1000 characters");
        }
    }
}
