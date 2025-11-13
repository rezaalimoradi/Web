namespace CMS.Application.Pages.Dtos
{
    public class PageTranslationDto
    {
        public long Id { get; set; }

        public long WebSiteLanguageId { get; set; }

        public string? LanguageName { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Content { get; set; }

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string CanonicalUrl { get; set; }
    }
}
