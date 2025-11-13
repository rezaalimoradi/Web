using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Localization.Entities;

namespace CMS.Domain.Shippings
{
    public class ShippingProvider : AggregateRoot
    {
        private readonly List<ShippingProviderTranslation> _translations = new();
        public IReadOnlyCollection<ShippingProviderTranslation> Translations => _translations.AsReadOnly();

        protected ShippingProvider() { } // EF

        public ShippingProvider(bool isEnabled = true)
        {
            IsEnabled = isEnabled;
            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        public bool IsEnabled { get; private set; }

        public string? ConfigureUrl { get; private set; }
        public string? LandingViewComponentName { get; private set; }

        public string? AdditionalSettings { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        // ---------- Behaviors ----------
        public ICollection<ShippingMethod> Methods { get; private set; } = new List<ShippingMethod>();

        // اختیاری: متد کمکی برای اضافه کردن
        public void AddMethod(ShippingMethod method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            Methods.Add(method);
        }
        public void UpdateConfiguration(string? configureUrl, string? landingViewComponentName, string? additionalSettings)
        {
            ConfigureUrl = configureUrl?.Trim();
            LandingViewComponentName = landingViewComponentName?.Trim();
            AdditionalSettings = additionalSettings?.Trim();
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Enable()
        {
            IsEnabled = true;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Disable()
        {
            IsEnabled = false;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        // ---------- Translations ----------

        public void AddTranslation(long languageId, string name, string description)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new ShippingProviderTranslation(Id, languageId, name, description));
        }

        public void UpdateTranslation(long languageId, string name, string description)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(name, description);
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
