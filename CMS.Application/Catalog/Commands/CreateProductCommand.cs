using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.Catalog.Commands
{
    public class CreateProductCommand : IAppRequest<ResultModel<long>>
    {
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
        public bool IsOriginal { get; set; }
        public string Barcode { get; set; }
        public decimal Weight { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public bool IsPublished { get; set; }
        public bool ShowOnHomepage { get; set; }
        public bool AllowCustomerReviews { get; set; }
        public bool IsCallForPrice { get; set; }
        public long? TaxId { get; set; }
        public long WebSiteId { get; set; }


        public List<IFormFile>? ImageFiles { get; set; }

        // Translation properties:
        public long WebSiteLanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }

        [Required(ErrorMessage = "حداقل یک دسته‌بندی انتخاب کنید.")]
        public List<long> CategoryIds { get; set; } = new();
    }
}
