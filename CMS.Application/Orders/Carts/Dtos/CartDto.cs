using CMS.Application.Orders.Carts.QueryHandlers;

namespace CMS.Application.Orders.Carts.Dtos
{
    public class CartDto
    {
        public long Id { get; set; }
        public long WebSiteId { get; set; }
        public string CustomerIdentifier { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public List<CartTranslationDto> Translations { get; set; }
    }
}
