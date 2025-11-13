using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Catalog.Entities
{
    /// <summary>
    /// رابطه‌ی بین محصول و ویژگی (مثلاً محصول "عطر دیور" دارای ویژگی "حجم" است)
    /// </summary>
    public class ProductProductAttribute : BaseEntity
    {
        #region Properties

        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public long ProductAttributeId { get; private set; }
        public ProductAttribute ProductAttribute { get; private set; }

        /// <summary>
        /// ترتیب نمایش ویژگی برای این محصول (مثلاً ابتدا رنگ، سپس حجم)
        /// </summary>
        public int DisplayOrder { get; private set; }

        /// <summary>
        /// مقادیر انتخاب‌شده‌ی این ویژگی برای این محصول (مثلاً 50ml, 100ml)
        /// </summary>
        public virtual ICollection<ProductProductAttribute_ValueMapping> ValueMappings { get; private set; }
            = new HashSet<ProductProductAttribute_ValueMapping>();

        #endregion

        #region Constructors

        protected ProductProductAttribute() { } // For EF

        public ProductProductAttribute(long productId, long productAttributeId, int displayOrder = 0)
        {
            if (productId <= 0)
                throw new DomainException("ProductId is required.");

            if (productAttributeId <= 0)
                throw new DomainException("ProductAttributeId is required.");

            if (displayOrder < 0)
                throw new DomainException("DisplayOrder cannot be negative.");

            ProductId = productId;
            ProductAttributeId = productAttributeId;
            DisplayOrder = displayOrder;
        }

        #endregion

        #region Behaviors

        public void SetDisplayOrder(int order)
        {
            if (order < 0)
                throw new DomainException("DisplayOrder cannot be negative.");

            DisplayOrder = order;
        }

        /// <summary>
        /// افزودن مقدار ویژگی به این ویژگیِ محصول (مثلاً افزودن 50ml یا قرمز)
        /// </summary>
        public void AddValueMapping(long attributeValueId)
        {
            if (ValueMappings.Any(vm => vm.ProductAttributeValueId == attributeValueId))
                throw new DomainException("This value is already mapped to the product attribute.");

            ValueMappings.Add(new ProductProductAttribute_ValueMapping(Id, attributeValueId));
        }

        /// <summary>
        /// حذف مقدار ویژگی از این ویژگیِ محصول
        /// </summary>
        public void RemoveValueMapping(long attributeValueId)
        {
            var mapping = ValueMappings.FirstOrDefault(vm => vm.ProductAttributeValueId == attributeValueId)
                ?? throw new DomainException("Value mapping not found.");

            ValueMappings.Remove(mapping);
        }

        #endregion
    }
}
