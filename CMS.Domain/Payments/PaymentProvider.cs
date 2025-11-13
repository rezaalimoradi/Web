using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMS.Domain.Payments
{
    public class PaymentProvider : BaseEntity
    {
        private readonly List<PaymentProviderTranslation> _translations = new();
        public IReadOnlyCollection<PaymentProviderTranslation> Translations => _translations.AsReadOnly();

        protected PaymentProvider() { } // For EF

        public PaymentProvider(string name, string configureUrl, string landingViewComponentName, bool isEnabled)
        {
            Update(name, configureUrl, landingViewComponentName, isEnabled);
        }

        [StringLength(450)]
        public string Name { get; private set; }

        public bool IsEnabled { get; private set; }

        [StringLength(450)]
        public string ConfigureUrl { get; private set; }

        [StringLength(450)]
        public string LandingViewComponentName { get; private set; }

        /// <summary>
        /// Additional settings for specific provider, stored as JSON string.
        /// </summary>
        public string AdditionalSettings { get; private set; }

        // --- Behaviors ---
        public void Update(string name, string configureUrl, string landingViewComponentName, bool isEnabled)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Provider name is required.");

            Name = name.Trim();
            ConfigureUrl = configureUrl?.Trim();
            LandingViewComponentName = landingViewComponentName?.Trim();
            IsEnabled = isEnabled;
        }

        public void SetAdditionalSettings(string json)
        {
            AdditionalSettings = json?.Trim();
        }

        // --- Translation Behaviors ---
        public void AddTranslation(long languageId, string name, string configureUrl, string landingViewComponentName)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new PaymentProviderTranslation(
                Id,
                languageId,
                name,
                configureUrl,
                landingViewComponentName
            ));
        }

        public void UpdateTranslation(long languageId, string name, string configureUrl, string landingViewComponentName)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(name, configureUrl, landingViewComponentName);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }
    }
}
