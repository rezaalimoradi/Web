using CMS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Tenants.Entitis
{
    /// <summary>
    /// Represents a domain assigned to a website, composed of a subdomain, domain name, and TLD (top-level domain).
    /// </summary>
    public partial class WebSiteDomain : BaseEntity
    {
        /// <summary>
        /// Gets or sets the main domain name (e.g., "example").
        /// </summary>
        public string DomainName { get; set; } = null!;

        public long WebSiteId { get; set; }

        [ForeignKey(nameof(WebSiteId))]
        public WebSite WebSite { get; set; }

        /// <summary>
        /// Gets or sets the ID of the top-level domain (e.g., ".com", ".net").
        /// </summary>
        public long TldId { get; set; }

        /// <summary>
        /// Gets or sets the top-level domain associated with this domain.
        /// </summary>
        public TopLevelDomain Tld { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether this domain is the default for its associated website.
        /// </summary>
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// Gets the full domain string composed of subdomain, domain name, and TLD.
        /// Example: "example.com"
        /// </summary>
        public string FullDomain => $"{DomainName}{Tld.Extension}";

        public WebSiteDomain() { }

        public WebSiteDomain(string domainName, long websiteId, TopLevelDomain tld, bool isDefault = false)
        {
            if (string.IsNullOrWhiteSpace(domainName))
                throw new ArgumentException("Domain name cannot be empty.", nameof(domainName));
            if (tld == null)
                throw new ArgumentNullException(nameof(tld));

            DomainName = domainName;
            WebSiteId = websiteId;
            Tld = tld;
            TldId = tld.Id;
            IsDefault = isDefault;
        }

        public void SetDefault(bool isDefault)
        {
            IsDefault = isDefault;
        }

        public void UpdateDomainName(string newDomainName)
        {
            if (string.IsNullOrWhiteSpace(newDomainName))
                throw new ArgumentException("Domain name cannot be empty.", nameof(newDomainName));
            DomainName = newDomainName;
        }
    }
}
