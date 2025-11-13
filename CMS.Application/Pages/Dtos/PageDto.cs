using CMS.Domain.Pages.Enums;

namespace CMS.Application.Pages.Dtos
{
    public class PageDto
    {
        public long Id { get; set; }

        public long WebSiteId { get; set; }

        public string? WebSiteTitle { get; set; }

        public PageStatus Status { get; set; }

        public DateTime? PublishAt { get; set; }

        public bool ShowInMenu { get; set; }

        public List<PageTranslationDto> Translations { get; set; } = new();
    }
}
