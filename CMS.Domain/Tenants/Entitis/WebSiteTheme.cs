using CMS.Domain.Common;
using CMS.Domain.Themes.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Tenants.Entitis
{
    public class WebSiteTheme : BaseEntity
    {
        public bool IsDefault { get; private set; }

        public long ThemeId { get; set; }

        public Theme Theme { get; set; }

        public long WebSiteId { get; set; }

        [ForeignKey(nameof(WebSiteId))]
        public WebSite WebSite { get; set; }

        public WebSiteTheme()
        {
        }

        public WebSiteTheme(bool isDefault, long themeId, long websiteId)
        {
            IsDefault = isDefault;
            ThemeId = themeId;
            WebSiteId = websiteId;
        }

        public void SetDefault(bool isDefault)
        {
            IsDefault = isDefault;
        }
    }
}
