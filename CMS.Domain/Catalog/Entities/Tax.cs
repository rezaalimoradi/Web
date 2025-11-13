using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Catalog.Entities;

public class Tax : AggregateRoot
{
    [Range(0, 1)]
    public decimal Rate { get; private set; }

    public bool? IsActive { get; private set; } = true;

    public long WebSiteId { get; private set; }

    [ForeignKey(nameof(WebSiteId))]
    public WebSite WebSite { get; private set; }

    public ICollection<TaxTranslation> Translations { get; set; } = [];

    public ICollection<Product> ProductTaxs { get; set; } = [];

    public Tax(long webSiteId, decimal rate)
    {
        WebSiteId = webSiteId;
        SetRate(rate);
    }

    public void SetRate(decimal rate)
    {
        if (rate < 0 || rate > 1)
            throw new DomainException("Rate must be between 0 and 1.");

        Rate = rate;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public void AddTranslation(long languageId, string name, string description)
    {
        if (Translations.Any(t => t.WebSiteLanguageId == languageId))
            throw new DomainException("Translation for this language already exists.");

        Translations.Add(new TaxTranslation(Id, languageId, name, description));
    }

    public void RemoveTranslation(long translationId)
    {
        var translation = Translations.FirstOrDefault(t => t.Id == translationId);
        if (translation == null)
            throw new DomainException("Translation not found.");

        Translations.Remove(translation);
    }

    public void UpdateTranslation(long languageId, string name, string description)
    {
        var translation = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
        if (translation == null)
            throw new DomainException("Translation not found.");

        translation.Update(name, description);
    }

    public void Update(decimal rate, bool isActive)
    {
        if (rate < 0 || rate > 1)
            throw new DomainException("Rate must be between 0 and 1.");

        Rate = rate;
        IsActive = isActive;
    }

}
