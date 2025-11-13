using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Catalog.Entities;

public class TaxTranslation : BaseEntity
{
    public long TaxId { get; private set; }

    public long WebSiteLanguageId { get; private set; }
    public WebSiteLanguage WebSiteLanguage { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public Tax Tax { get; private set; }

    public TaxTranslation(long taxId, long webSiteLanguageId, string name, string description)
    {
        TaxId = taxId;
        WebSiteLanguageId = webSiteLanguageId;
        Name = name;
        Description = description;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
