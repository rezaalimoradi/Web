namespace CMS.Application.Catalog.Dtos
{
    public class BrandDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        public long WebSiteLanguageId { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public string? CanonicalUrl { get; set; }
    }
}
