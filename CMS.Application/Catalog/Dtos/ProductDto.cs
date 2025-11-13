using CMS.Domain.Catalog.Enums;
using Microsoft.AspNetCore.Http;

namespace CMS.Application.Catalog.Dtos
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string SKU { get; set; }

        public long? BrandId { get; set; }
        public string? BrandTitle { get; set; }
        public long CategoryId { get; set; }
        public string? CategoryTitle { get; set; }
        public bool ManageInventory { get; set; }

        public ProductType Type { get; set; }
        public bool? IsOriginal { get; set; }

        public string Barcode { get; set; }

        public decimal? Weight { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }

        public bool? IsPublished { get; set; }
        public bool? ShowOnHomepage { get; set; }
        public bool? AllowCustomerReviews { get; set; }
        public bool? IsCallForPrice { get; set; }

        public long? TaxId { get; set; }
        public long WebSiteId { get; set; }

        // فیلدهای ترجمه‌شده
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }

        public decimal Price { get; set; }
        public decimal? SpecialPrice { get; set; }
        public DateTime? SpecialPriceStart { get; set; }
        public DateTime? SpecialPriceEnd { get; set; }

        public string Values { get; set; }

        public long? WebSiteLanguageId { get; set; }

        public List<string> ImageUrls { get; set; } = new();

        public List<ProductAttributeDto> Attributes { get; set; } = new();
    }
}
