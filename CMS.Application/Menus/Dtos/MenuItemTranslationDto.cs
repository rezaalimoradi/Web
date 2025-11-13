namespace CMS.Application.Menus.Dtos
{
    public class MenuItemTranslationDto
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Link { get; set; } = string.Empty;

        public long MenuItemId { get; set; }

        public long WebSiteLanguageId { get; set; }

        public string? LanguageCode { get; set; }
    }
}
