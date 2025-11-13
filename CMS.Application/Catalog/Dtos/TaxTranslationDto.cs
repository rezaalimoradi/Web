namespace CMS.Application.Catalog.Dtos
{
    public class TaxTranslationDto
    {
        public long Id { get; set; }
        public long TaxId { get; set; }
        public long LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
