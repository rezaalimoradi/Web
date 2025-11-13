using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Domain.Orders
{
    public class OrderAddress : BaseEntity
    {
        private readonly List<OrderAddressTranslation> _translations = new();
        public IReadOnlyCollection<OrderAddressTranslation> Translations => _translations.AsReadOnly();

        // ---------------- Constructors ----------------
        protected OrderAddress() { } // For EF

        public OrderAddress(string phone, string zipCode)
        {
            Phone = SafeTrim(phone);
            ZipCode = SafeTrim(zipCode);
            ValidateMaxLength(Phone, nameof(Phone));
            ValidateMaxLength(ZipCode, nameof(ZipCode));
        }

        // ---------------- Properties ----------------
        public string Phone { get; private set; }
        public string ZipCode { get; private set; }

        // ---------------- Translation behaviors ----------------
        public void AddTranslation(
            long languageId,
            string contactName,
            string addressLine1,
            string addressLine2,
            string city)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new OrderAddressTranslation(
                Id,
                languageId,
                contactName,
                addressLine1,
                addressLine2,
                city
            ));
        }

        public void UpdateTranslation(
            long languageId,
            string contactName,
            string addressLine1,
            string addressLine2,
            string city)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(contactName, addressLine1, addressLine2, city);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }

        // ---------------- Helpers ----------------
        private static string SafeTrim(string value) => value?.Trim();

        private void ValidateMaxLength(string value, string propName)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 450)
                throw new DomainException($"{propName} must be at most 450 characters.");
        }
    }
}
