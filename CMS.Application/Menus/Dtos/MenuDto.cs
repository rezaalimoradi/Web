namespace CMS.Application.Menus.Dtos
{
    public class MenuDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public long WebSiteId { get; set; }

        public List<MenuItemDto> Items { get; set; } = new();
    }
}
