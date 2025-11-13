using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMS.Domain.Checkouts
{
    public class CheckoutAddress : BaseEntity
    {
        private readonly List<CheckoutAddressTranslation> _translations = new();
        public IReadOnlyCollection<CheckoutAddressTranslation> Translations => _translations.AsReadOnly();

        protected CheckoutAddress() { } // EF

        public CheckoutAddress(
            string contactName,
            string phone,
            string addressLine1,
            string addressLine2,
            string city,
            string zipCode)
        {
            Update(contactName, phone, addressLine1, addressLine2, city, zipCode);
        }

        [StringLength(450)]
        public string ContactName { get; private set; }

        [StringLength(450)]
        public string Phone { get; private set; }

        [StringLength(450)]
        public string AddressLine1 { get; private set; }

        [StringLength(450)]
        public string AddressLine2 { get; private set; }

        [StringLength(450)]
        public string City { get; private set; }

        [StringLength(450)]
        public string ZipCode { get; private set; }

        // --- Behaviors ---
        public void Update(
            string contactName,
            string phone,
            string addressLine1,
            string addressLine2,
            string city,
            string zipCode)
        {
            if (string.IsNullOrWhiteSpace(addressLine1))
                throw new DomainException("AddressLine1 is required.");
            if (string.IsNullOrWhiteSpace(city))
                throw new DomainException("City is required.");

            ContactName = SafeTrim(contactName);
            Phone = SafeTrim(phone);
            AddressLine1 = SafeTrim(addressLine1);
            AddressLine2 = SafeTrim(addressLine2);
            City = SafeTrim(city);
            ZipCode = SafeTrim(zipCode);

            ValidateMaxLength(ContactName, nameof(ContactName));
            ValidateMaxLength(Phone, nameof(Phone));
            ValidateMaxLength(AddressLine1, nameof(AddressLine1));
            ValidateMaxLength(AddressLine2, nameof(AddressLine2));
            ValidateMaxLength(City, nameof(City));
            ValidateMaxLength(ZipCode, nameof(ZipCode));
        }

        public void AddTranslation(long languageId, string city, string addressLine1, string addressLine2)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new CheckoutAddressTranslation(Id, languageId, city, addressLine1, addressLine2));
        }

        public void UpdateTranslation(long languageId, string city, string addressLine1, string addressLine2)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(city, addressLine1, addressLine2);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }

        private static string SafeTrim(string value) => value?.Trim();

        private void ValidateMaxLength(string value, string propName)
        {
            if (value != null && value.Length > 450)
                throw new DomainException($"{propName} must be at most 450 characters.");
        }
    }
}
