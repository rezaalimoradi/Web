using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.OrderHistories;
using CMS.Domain.Users.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CMS.Domain.Orders
{
    public class Order : AggregateRoot
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private readonly List<Order> _children = new();
        public IReadOnlyCollection<Order> Children => _children.AsReadOnly();

        private readonly List<OrderTranslation> _translations = new();
        public IReadOnlyCollection<OrderTranslation> Translations => _translations.AsReadOnly();

        private readonly List<OrderHistory> _histories = new();
        public IReadOnlyCollection<OrderHistory> Histories => _histories.AsReadOnly();

        protected Order() { } // For EF

        public Order(long customerId, long createdById, long shippingAddressId, long billingAddressId)
        {
            CustomerId = ValidateId(customerId, nameof(customerId));
            CreatedById = ValidateId(createdById, nameof(createdById));
            ShippingAddressId = ValidateId(shippingAddressId, nameof(shippingAddressId));
            BillingAddressId = ValidateId(billingAddressId, nameof(billingAddressId));
            LatestUpdatedById = createdById;
            CouponCode = "-";
            CouponRuleName = "_";
            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
            OrderStatus = OrderStatus.New;
            IsMasterOrder = false;
        }

        public long CustomerId { get; private set; }
        [JsonIgnore] public AppUser Customer { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public long CreatedById { get; private set; }
        [JsonIgnore] public AppUser CreatedBy { get; private set; }

        public DateTimeOffset LatestUpdatedOn { get; private set; }
        public long LatestUpdatedById { get; private set; }
        [JsonIgnore] public AppUser LatestUpdatedBy { get; private set; }

        public long? VendorId { get; private set; }

        [StringLength(450)] public string CouponCode { get; private set; }
        [StringLength(450)] public string CouponRuleName { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal SubTotalWithDiscount { get; private set; }

        public long ShippingAddressId { get; private set; }
        public OrderAddress ShippingAddress { get; private set; }

        public long BillingAddressId { get; private set; }
        public OrderAddress BillingAddress { get; private set; }

        public OrderStatus OrderStatus { get; private set; }

        public long? ParentId { get; private set; }
        [JsonIgnore] public Order Parent { get; private set; }
        public bool IsMasterOrder { get; private set; }

        public decimal ShippingFeeAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal OrderTotal { get; private set; }
        public decimal PaymentFeeAmount { get; private set; }

        // ---------------- Behaviors ----------------

        public void AddOrderItem(long productId, int quantity, decimal unitPrice, long updatedById)
        {
            ValidateQuantityAndPrice(quantity, unitPrice);

            var item = new OrderItem(Id, productId, unitPrice, quantity);
            _orderItems.Add(item);

            RecalculateTotals();
            MarkUpdated(updatedById);
            AddHistory(OrderHistory.ItemAdded(Id, productId, quantity, updatedById));
        }

        public void RemoveOrderItem(long orderItemId, long updatedById)
        {
            var item = _orderItems.FirstOrDefault(x => x.Id == orderItemId)
                       ?? throw new DomainException("Order item not found.");

            _orderItems.Remove(item);

            RecalculateTotals();
            MarkUpdated(updatedById);
            AddHistory(OrderHistory.ItemRemoved(Id, orderItemId, updatedById));
        }

        public void ApplyCoupon(string code, string ruleName, decimal discountAmount, long updatedById)
        {
            if (discountAmount < 0)
                throw new DomainException("Discount cannot be negative.");

            CouponCode = code;
            CouponRuleName = ruleName;
            DiscountAmount = discountAmount;

            RecalculateTotals();
            MarkUpdated(updatedById);
            AddHistory(OrderHistory.DiscountApplied(Id, code, discountAmount, updatedById));
        }

        public void ChangeStatus(OrderStatus newStatus, long updatedById, string note = null)
        {
            if (OrderStatus == newStatus) return;

            var oldStatus = OrderStatus;
            OrderStatus = newStatus;

            MarkUpdated(updatedById);
            AddHistory(OrderHistory.StatusChanged(Id, oldStatus, newStatus, note, updatedById));
        }

        public void AddChildOrder(Order child, long updatedById)
        {
            if (child == null)
                throw new DomainException("Child order is required.");

            child.ParentId = Id;
            _children.Add(child);

            MarkUpdated(updatedById);
            AddHistory(OrderHistory.ChildOrderAdded(Id, child.Id, updatedById));
        }

        public void AddTranslation(long languageId, string orderNote, string shippingMethod, string paymentMethod, long updatedById)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new OrderTranslation(Id, languageId, orderNote, shippingMethod, paymentMethod));
            MarkUpdated(updatedById);
        }

        public void UpdateTranslation(long languageId, string orderNote, string shippingMethod, string paymentMethod, long updatedById)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                              ?? throw new DomainException("Translation not found.");

            translation.Update(orderNote, shippingMethod, paymentMethod);
            MarkUpdated(updatedById);
        }

        public void RemoveTranslation(long languageId, long updatedById)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                              ?? throw new DomainException("Translation not found.");

            _translations.Remove(translation);
            MarkUpdated(updatedById);
        }

        // 🟢 جدید: مخصوص افزودن تاریخچه بعد از ساخت سفارش
        public void AddCreatedHistory(long createdById)
        {
            AddHistory(OrderHistory.StatusChanged(Id, null, OrderStatus.New, "Order created", createdById));
        }

        // ---------------- Private Helpers ----------------

        private void RecalculateTotals()
        {
            SubTotal = _orderItems.Sum(i => i.ProductPrice * i.Quantity);
            SubTotalWithDiscount = Math.Max(0, SubTotal - DiscountAmount);
            OrderTotal = SubTotalWithDiscount + TaxAmount + ShippingFeeAmount + PaymentFeeAmount;
        }

        private void AddHistory(OrderHistory history) => _histories.Add(history);

        private void MarkUpdated(long updatedById)
        {
            LatestUpdatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedById = updatedById;
        }

        private static long ValidateId(long id, string paramName)
        {
            if (id <= 0) throw new DomainException($"{paramName} is required.");
            return id;
        }

        private static void ValidateQuantityAndPrice(int quantity, decimal unitPrice)
        {
            if (quantity <= 0) throw new DomainException("Quantity must be greater than zero.");
            if (unitPrice < 0) throw new DomainException("Unit price cannot be negative.");
        }
    }
}
