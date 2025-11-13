using CMS.Domain.Catalog.Enums;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Media.Entities;
using CMS.Domain.Tenants.Entitis;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Catalog.Entities
{
    public class Product : AggregateRoot
    {
        #region Properties

        // --- Pricing ---
        public decimal Price { get; private set; }
        public decimal? SpecialPrice { get; private set; }
        public DateTime? SpecialPriceStart { get; private set; }
        public DateTime? SpecialPriceEnd { get; private set; }

        // --- Identification ---
        public string SKU { get; private set; } = default!;
        public string Barcode { get; private set; } = string.Empty;

        // --- Dimensions ---
        public decimal? Width { get; private set; }
        public decimal? Height { get; private set; }
        public decimal? Length { get; private set; }

        // --- Relations ---
        public long? BrandId { get; private set; }
        public long WebSiteId { get; private set; }
        public long? TaxId { get; private set; }

        public Brand? Brand { get; private set; }
        public WebSite? WebSite { get; private set; }
        public Tax? Tax { get; private set; }

        // --- Inventory ---
        public int StockQuantity { get; private set; }
        public bool ManageInventory { get; private set; } = true;
        public bool IsInStock => !ManageInventory || StockQuantity > 0;

        // --- Product Info ---
        public ProductType Type { get; private set; }
        public bool? IsOriginal { get; private set; } = false;
        public bool? IsPublished { get; private set; }
        public bool? ShowOnHomepage { get; private set; }
        public bool? AllowCustomerReviews { get; private set; }
        public bool? IsCallForPrice { get; private set; } = false;

        // --- Collections ---
        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
        public ICollection<ProductTranslation> Translations { get; private set; } = new List<ProductTranslation>();
        public ICollection<ProductRelation> RelatedProducts { get; private set; } = new List<ProductRelation>();
        public ICollection<ProductProductAttribute> ProductProductAttributes { get; private set; } = new List<ProductProductAttribute>();
        public ICollection<Product_ProductCategory_Mapping> Product_ProductCategories { get; private set; } = new List<Product_ProductCategory_Mapping>();

        // ---------- Media ----------
        public virtual ICollection<MediaAttachment> MediaAttachments { get; private set; } = new List<MediaAttachment>();

        [NotMapped]
        public MediaAttachment? MainImage => MediaAttachments.FirstOrDefault(m => m.Purpose == "Main");

        [NotMapped]
        public IEnumerable<MediaAttachment> GalleryImages => MediaAttachments.Where(m => m.Purpose == "Gallery");

        #endregion

        #region Constructors

        protected Product() { }

        public Product(
            decimal price,
            string sku,
            ProductType type,
            bool isOriginal,
            decimal width,
            decimal height,
            decimal length,
            long websiteId,
            long? brandId = null,
            long? taxId = null,
            decimal? specialPrice = null,
            DateTime? specialPriceStart = null,
            DateTime? specialPriceEnd = null,
            bool isPublished = false,
            bool showOnHomepage = false,
            bool allowCustomerReviews = false,
            bool isCallForPrice = false,
            int stockQuantity = 0,
            bool manageInventory = true,
            string? barcode = null
        )
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new DomainException("SKU cannot be empty.");
            if (price < 0)
                throw new DomainException("Price cannot be negative.");

            Price = price;
            SKU = sku.Trim();
            Type = type;
            IsOriginal = isOriginal;
            Width = width;
            Height = height;
            Length = length;
            WebSiteId = websiteId;
            BrandId = brandId;
            TaxId = taxId;
            SpecialPrice = specialPrice;
            SpecialPriceStart = specialPriceStart;
            SpecialPriceEnd = specialPriceEnd;
            IsPublished = isPublished;
            ShowOnHomepage = showOnHomepage;
            AllowCustomerReviews = allowCustomerReviews;
            IsCallForPrice = isCallForPrice;
            StockQuantity = stockQuantity;
            ManageInventory = manageInventory;
            Barcode = barcode?.Trim() ?? string.Empty;
        }

        #endregion

        #region Update Method

        public void Update(
            string sku,
            long? brandId,
            bool manageInventory,
            ProductType type,
            bool? isOriginal,
            string? barcode,
            decimal? width,
            decimal? height,
            decimal? length,
            bool? isPublished,
            bool? showOnHomepage,
            bool? allowCustomerReviews,
            bool? isCallForPrice,
            long? taxId
        )
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new DomainException("SKU cannot be empty.");

            SKU = sku.Trim();
            BrandId = brandId;
            ManageInventory = manageInventory;
            Type = type;
            IsOriginal = isOriginal;
            Barcode = barcode?.Trim() ?? string.Empty;
            Width = width;
            Height = height;
            Length = length;
            IsPublished = isPublished;
            ShowOnHomepage = showOnHomepage;
            AllowCustomerReviews = allowCustomerReviews;
            IsCallForPrice = isCallForPrice;
            TaxId = taxId;

            SetUpdatedAt();
        }

        #endregion

        #region Translation Behavior

        public void AddTranslation(long languageId, string name, string description, string slug)
        {
            if (Translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            Translations.Add(new ProductTranslation(Id, languageId, name, description, slug));
        }

        public void UpdateTranslation(long languageId, string name, string description, string slug)
        {
            var translation = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                              ?? throw new DomainException("Translation not found.");

            translation.Update(name, description, slug);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                              ?? throw new DomainException("Translation not found.");

            Translations.Remove(translation);
        }

        #endregion

        #region Related Product Behavior

        public void AddRelation(long relatedProductId)
        {
            if (relatedProductId <= 0)
                throw new DomainException("Invalid RelatedProductId.");
            if (Id == relatedProductId)
                throw new DomainException("A product cannot be related to itself.");
            if (RelatedProducts.Any(r => r.RelatedProductId == relatedProductId))
                throw new DomainException("This product is already related.");

            RelatedProducts.Add(new ProductRelation(Id, relatedProductId));
        }

        public void RemoveRelation(long relatedProductId)
        {
            var relation = RelatedProducts.FirstOrDefault(r => r.RelatedProductId == relatedProductId)
                           ?? throw new DomainException("Relation not found.");
            RelatedProducts.Remove(relation);
        }

        #endregion

        #region Pricing Behavior

        public decimal GetEffectivePrice(IEnumerable<long>? selectedValueIds = null)
        {
            var finalPrice = GetBasePrice();

            if (selectedValueIds == null || !selectedValueIds.Any() || ProductProductAttributes == null || !ProductProductAttributes.Any())
                return finalPrice;

            var selectedValues = ProductProductAttributes
                .SelectMany(ppa => ppa.ValueMappings ?? Enumerable.Empty<ProductProductAttribute_ValueMapping>())
                .Where(vm => vm.ProductAttributeValueId.HasValue)
                .Select(vm => vm.ProductAttributeValue)
                .Where(v => v != null && selectedValueIds.Contains(v.Id))
                .ToList();

            foreach (var value in selectedValues)
                finalPrice += value.PriceAdjustment ?? 0m;

            return finalPrice;
        }

        private decimal GetBasePrice()
        {
            if (SpecialPrice.HasValue &&
                (!SpecialPriceStart.HasValue || SpecialPriceStart <= DateTime.UtcNow) &&
                (!SpecialPriceEnd.HasValue || SpecialPriceEnd >= DateTime.UtcNow))
            {
                return SpecialPrice.Value;
            }

            return Price;
        }

        #endregion

        // ---------- Media ----------
        public void AddMainImage(MediaAttachment attachment)
        {
            ValidateAttachment(attachment);
            if (MediaAttachments.Any(m => m.Purpose == "Main"))
                throw new DomainException("This product already has a main image."); 
            attachment.SetPurpose("Main");
            MediaAttachments.Add(attachment);
        }

        public void AddGalleryImage(MediaAttachment attachment)
        {
            ValidateAttachment(attachment);
            attachment.SetPurpose("Gallery");
            MediaAttachments.Add(attachment);
        }

        public void RemoveImage(long mediaAttachmentId)
        {
            var image = MediaAttachments.FirstOrDefault(m => m.Id == mediaAttachmentId)
                        ?? throw new DomainException("Image not found.");
            MediaAttachments.Remove(image);
        }

        private static void ValidateAttachment(MediaAttachment attachment)
        {
            if (attachment == null) throw new DomainException("Attachment cannot be null.");
        }
    }
}
