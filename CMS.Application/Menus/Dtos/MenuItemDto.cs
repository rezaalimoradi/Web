namespace CMS.Application.Menus.Dtos
{
    public class MenuItemDto
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public long? ParentId { get; set; }
    }
}
