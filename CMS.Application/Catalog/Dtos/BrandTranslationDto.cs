namespace CMS.Application.Catalog.Dtos
{
    public class BrandTranslationDto
    {
        public long Id { get; set; }
        public long BrandId { get; set; }
        public long LanguageId { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
    }
}
