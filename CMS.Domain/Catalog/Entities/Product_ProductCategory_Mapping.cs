using CMS.Domain.Common;

namespace CMS.Domain.Catalog.Entities
{
    public class Product_ProductCategory_Mapping : BaseEntity
    {
        public long ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
