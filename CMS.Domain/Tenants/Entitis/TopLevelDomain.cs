using CMS.Domain.Common;

namespace CMS.Domain.Tenants.Entitis
{
    /// <summary>
    /// Represents a top-level domain (e.g., ".com", ".ir").
    /// </summary>
    public partial class TopLevelDomain : BaseEntity
    {
        /// <summary>
        /// TLD string (e.g., ".com", ".net").
        /// </summary>
        public string Extension { get; set; } = null!;

        /// <summary>
        /// Whether this TLD is allowed for use.
        /// </summary>
        public bool IsAllowed { get; set; } = true;

        /// <summary>
        /// List of tenant domains using this TLD.
        /// </summary>
        public ICollection<WebSiteDomain> Domains { get; set; } = new List<WebSiteDomain>();
    }
}
