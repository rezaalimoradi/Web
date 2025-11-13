using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Catalog.Entities
{
    /// <summary>
    /// نگاشت بین ویژگی محصول و مقدار ویژگی (مثلاً: "ویژگی حجم" → "100ml")
    /// </summary>
    public class ProductProductAttribute_ValueMapping : BaseEntity
    {
        #region Properties

        public long ProductProductAttributeId { get; private set; }
        public ProductProductAttribute ProductProductAttribute { get; private set; } = default!;

        /// <summary>
        /// شناسه‌ی مقدار ویژگی (در صورت انتخاب از لیست ثابت)
        /// </summary>
        public long? ProductAttributeValueId { get; private set; }
        public ProductAttributeValue? ProductAttributeValue { get; private set; }

        /// <summary>
        /// مقدار سفارشی (در صورتی که مقدار از جدول مقادیر ثابت نباشد)
        /// </summary>
        public string? CustomValue { get; private set; }

        /// <summary>
        /// تأثیر این مقدار ویژگی بر روی قیمت محصول
        /// </summary>
        public decimal? PriceAdjustment { get; private set; }

        /// <summary>
        /// تأثیر این مقدار ویژگی بر روی وزن محصول (اختیاری)
        /// </summary>
        public decimal? WeightAdjustment { get; private set; }

        #endregion

        #region Constructors

        protected ProductProductAttribute_ValueMapping() { } // EF

        /// <summary>
        /// نگاشت مقدار از جدول مقادیر ویژگی
        /// </summary>
        public ProductProductAttribute_ValueMapping(long productProductAttributeId, long productAttributeValueId,
            decimal? priceAdjustment = null, decimal? weightAdjustment = null)
        {
            if (productProductAttributeId <= 0)
                throw new DomainException("Invalid ProductProductAttributeId.");
            if (productAttributeValueId <= 0)
                throw new DomainException("Invalid ProductAttributeValueId.");

            ProductProductAttributeId = productProductAttributeId;
            ProductAttributeValueId = productAttributeValueId;
            PriceAdjustment = priceAdjustment;
            WeightAdjustment = weightAdjustment;
        }

        /// <summary>
        /// نگاشت مقدار سفارشی (در صورتی که مقدار از جدول نباشد)
        /// </summary>
        public ProductProductAttribute_ValueMapping(long productProductAttributeId, string customValue,
            decimal? priceAdjustment = null, decimal? weightAdjustment = null)
        {
            if (productProductAttributeId <= 0)
                throw new DomainException("Invalid ProductProductAttributeId.");
            if (string.IsNullOrWhiteSpace(customValue))
                throw new DomainException("Custom value cannot be empty.");

            ProductProductAttributeId = productProductAttributeId;
            CustomValue = customValue;
            PriceAdjustment = priceAdjustment;
            WeightAdjustment = weightAdjustment;
        }

        #endregion

        #region Behaviors

        public void UpdatePriceAdjustment(decimal? price)
        {
            if (price < 0)
                throw new DomainException("Price adjustment cannot be negative.");

            PriceAdjustment = price;
        }

        public void UpdateWeightAdjustment(decimal? weight)
        {
            if (weight < 0)
                throw new DomainException("Weight adjustment cannot be negative.");

            WeightAdjustment = weight;
        }

        public void UpdateCustomValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Custom value cannot be empty.");

            CustomValue = value;
        }

        #endregion
    }
}
