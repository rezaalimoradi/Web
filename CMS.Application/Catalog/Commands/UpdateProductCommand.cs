using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Enums;
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.Catalog.Commands
{
    public class UpdateProductCommand : IAppRequest<ResultModel>
    {
        public long Id { get; set; }

        public decimal Price { get; set; }
        public decimal? SpecialPrice { get; set; }
        public DateTime? SpecialPriceStart { get; set; }
        public DateTime? SpecialPriceEnd { get; set; }

        public string SKU { get; set; }
        public long? BrandId { get; set; }
        public long CategoryId { get; set; }

        public int StockQuantity { get; set; }
        public bool ManageInventory { get; set; }
        public ProductType Type { get; set; }
        public bool? IsOriginal { get; set; }
        public string Barcode { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }

        public bool IsPublished { get; set; } = false;
        public bool ShowOnHomepage { get; set; } = false;
        public bool AllowCustomerReviews { get; set; } = false;
        public bool IsCallForPrice { get; set; } = false;
        public long? TaxId { get; set; }
        public long WebSiteId { get; set; }

        // Translation props
        public long WebSiteLanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }

        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public string? CanonicalUrl { get; set; }

        [Required(ErrorMessage = "حداقل یک دسته‌بندی انتخاب کنید.")]
        public List<long> CategoryIds { get; set; } = new();
    }
}
