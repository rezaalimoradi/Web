namespace CMS.Application.Orders.Carts.Dtos
{
    public class CartUpdateResultDto
    {
        public long CartId { get; set; }
        public long ItemId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal CartTotal { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
