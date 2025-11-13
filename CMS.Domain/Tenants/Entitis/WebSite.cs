using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.ValueObjects;
using CMS.Domain.Users.Entities;
using System.Collections.ObjectModel;

namespace CMS.Domain.Tenants.Entitis
{
    /// <summary>
    /// Represents a website instance within the system.
    /// </summary>
    public partial class WebSite : AggregateRoot
    {
        /// <summary>
        /// Gets or sets the contact information for the website.
        /// </summary>
        public ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the URL of the website's logo.
        /// </summary>
        public string? LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the company name associated with the website.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the status of the website (active or inactive).
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the description of the website.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The ID of the website's owner.
        /// </summary>
        public long? OwnerId { get; set; }

        /// <summary>
        /// Navigation property for the owner of the website.
        /// </summary>
        public AppUser? Owner { get; set; }

        private readonly List<WebSiteDomain> _domains = new();
        public IReadOnlyCollection<WebSiteDomain> Domains => _domains.AsReadOnly();

        private readonly List<WebSiteLanguage> _supportedLanguages = new();
        public IReadOnlyCollection<WebSiteLanguage> SupportedLanguages => _supportedLanguages.AsReadOnly();

        private readonly List<WebSiteTheme> _themes = new();
        public IReadOnlyCollection<WebSiteTheme> Themes => _themes.AsReadOnly();

        public WebSite() { }

        public WebSite(ContactInfo contactInfo, string? logoUrl, string companyName, bool isActive, string? description)
        {
            ContactInfo = contactInfo;
            LogoUrl = logoUrl;
            CompanyName = companyName;
            IsActive = isActive;
            Description = description;
        }

        #region Domain Management

        public void AddDomain(WebSiteDomain domain)
        {
            if (_domains.Any(d => d.FullDomain == domain.FullDomain))
                throw new InvalidOperationException("Domain already exists for this website.");

            _domains.Add(domain);
        }

        public void RemoveDomain(long domainId)
        {
            var domain = _domains.FirstOrDefault(d => d.Id == domainId);
            if (domain != null)
            {
                _domains.Remove(domain);
            }
        }

        public void SetDefaultDomain(long domainId)
        {
            foreach (var domain in _domains)
            {
                domain.SetDefault(domain.Id == domainId);
            }
        }

        public WebSiteDomain? GetDefaultDomain() => _domains.FirstOrDefault(d => d.IsDefault);

        #endregion

        #region Language Management

        public void AddLanguage(WebSiteLanguage websiteLanguage)
        {
            if (_supportedLanguages.Any(x => x.LanguageId == websiteLanguage.LanguageId))
                throw new InvalidOperationException("Language already added to website.");

            _supportedLanguages.Add(websiteLanguage);
        }

        public void RemoveLanguage(long languageId)
        {
            var lang = _supportedLanguages.FirstOrDefault(x => x.LanguageId == languageId);
            if (lang != null)
            {
                _supportedLanguages.Remove(lang);
            }
        }

        public void SetDefaultLanguage(long languageId)
        {
            foreach (var lang in _supportedLanguages)
            {
                lang.SetDefault(lang.LanguageId == languageId);
            }
        }

        public WebSiteLanguage? GetDefaultLanguage()
        {
            return _supportedLanguages.SingleOrDefault(x => x.IsDefault) ?? _supportedLanguages.FirstOrDefault();
        }

        #endregion

        #region Theme Management (Optional)

        public void AddTheme(WebSiteTheme theme)
        {
            if (_themes.Any(x => x.ThemeId == theme.ThemeId))
                throw new InvalidOperationException("Theme already exists.");

            _themes.Add(theme);
        }

        public void SetDefaultTheme(long themeId)
        {
            foreach (var theme in _themes)
            {
                theme.SetDefault(theme.ThemeId == themeId);
            }
        }

        public void RemoveTheme(long themeId)
        {
            var theme = _themes.FirstOrDefault(x => x.ThemeId == themeId);
            if (theme != null)
            {
                _themes.Remove(theme);
            }
        }

        public WebSiteTheme? GetDefaultTheme() => _themes.FirstOrDefault(t => t.IsDefault);

        #endregion
    }
}
