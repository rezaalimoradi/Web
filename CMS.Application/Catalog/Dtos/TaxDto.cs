namespace CMS.Application.Catalog.Dtos
{
    public class TaxDto
    {
        public long Id { get; set; }
        public decimal Rate { get; set; }
        public bool? IsActive { get; set; }
        public long WebSiteId { get; set; }
    }
}
