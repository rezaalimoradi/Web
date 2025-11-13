using CMS.Domain.Blog.Enums;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Catalog.Entities;

public class Brand : AggregateRoot
{
    public long WebSiteId { get; private set; }

    [ForeignKey(nameof(WebSiteId))]
    public WebSite WebSite { get; private set; }

    public ICollection<BrandTranslation> Translations { get; private set; } = new List<BrandTranslation>();

    public ICollection<Product> ProductBrands { get; set; } = [];

    protected Brand() { }

    public Brand(long webSiteId)
    {
        if (webSiteId <= 0)
            throw new DomainException("Invalid WebSiteId.");
        WebSiteId = webSiteId;
    }

    public static Brand Create(long webSiteId)
    {
        return new Brand(webSiteId);
    }

    public void AddTranslation(long brandId,long webSiteLanguageId, string title, string? description,string? metaKeywords, string? metaDescription, string slug,string? metaTitle, string? canonicalUrl)
    {
        if (Translations.Any(t => t.WebSiteLanguageId == webSiteLanguageId))
            throw new DomainException("Translation for this language already exists.");

        var translation = new BrandTranslation(
            brandId,
            title,
            description,
            slug,
            webSiteLanguageId,
            metaTitle,
            metaDescription,
            metaKeywords,
            canonicalUrl
        )
        {
            Brand = this
        };
        Translations.Add(translation);
    }

    public void RemoveTranslation(long translationId)
    {
        var existing = Translations.FirstOrDefault(x => x.Id == translationId);

        if (existing == null)
            throw new DomainException("Translation not found.");

        Translations.Remove(existing);
    }

    public void AddOrUpdateTranslation(
        long brandId,
        long languageId,
        string title,
        string? description,
        string slug,
        string metaDescription,
        string metaTitle,
        string metaKeywords,
        string canonicalUrl
        )
    {
        var translation = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
        if (translation == null)
            AddTranslation(brandId,languageId, title, description, slug, metaKeywords, metaDescription, metaTitle, canonicalUrl);
        else
        {
            translation.Title = title;
            translation.Description = description;
            translation.Slug = slug;
            translation.MetaKeywords = metaKeywords;
            translation.MetaDescription = metaDescription;
            translation.MetaTitle = metaTitle;
            translation.CanonicalUrl = canonicalUrl;
        }
    }

}
