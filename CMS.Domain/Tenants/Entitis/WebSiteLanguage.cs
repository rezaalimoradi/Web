using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Tenants.Entitis
{
    public partial class WebSiteLanguage : BaseEntity
    {
        public long LanguageId { get; set; }

        public bool IsDefault { get; set; }

        public long WebSiteId { get; set; }

        [ForeignKey(nameof(WebSiteId))]
        public WebSite WebSite { get; set; }

        public Language Language { get; set; }

        public WebSiteLanguage() { }

        public WebSiteLanguage(long websiteId, long languageId, bool isDefault)
        {
            WebSiteId = websiteId;
            LanguageId = languageId;
            IsDefault = isDefault;
        }

        public void SetDefault(bool isDefault)
        {
            IsDefault = isDefault;
        }
    }
}
