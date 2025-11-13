using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders;
using CMS.Domain.Users.Entities;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.OrderHistories
{
    public class OrderHistory : BaseEntity
    {
        // ---------------- Properties ----------------
        public long OrderId { get; private set; }
        public Order Order { get; private set; }

        public OrderStatus? OldStatus { get; private set; }
        public OrderStatus NewStatus { get; private set; }

        [StringLength(1000)]
        public string Note { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }

        public long CreatedById { get; private set; }
        public AppUser CreatedBy { get; private set; }

        [StringLength(450)]
        public string EventType { get; private set; }

        public string Metadata { get; private set; }

        // ---------------- Constructors ----------------
        protected OrderHistory() { } // For EF Core

        private OrderHistory(
            long orderId,
            OrderStatus? oldStatus,
            OrderStatus newStatus,
            string note,
            long createdById,
            string eventType,
            string metadata = null)
        {
            OrderId = orderId > 0 ? orderId : throw new DomainException("OrderId is required.");
            OldStatus = oldStatus;
            NewStatus = newStatus;
            Note = note?.Trim();
            CreatedOn = DateTimeOffset.UtcNow;
            CreatedById = createdById;
            EventType = string.IsNullOrWhiteSpace(eventType) ? OrderHistoryEventTypes.StatusChanged : eventType;
            Metadata = metadata ?? " ";
        }

        // ---------------- Factory Methods ----------------
        public static OrderHistory StatusChanged(long orderId, OrderStatus? oldStatus, OrderStatus newStatus, string note, long createdById)
            => new(orderId, oldStatus, newStatus, note, createdById, OrderHistoryEventTypes.StatusChanged);

        public static OrderHistory DiscountApplied(long orderId, string couponCode, decimal discountAmount, long createdById)
        {
            var metadata = $"{{\"CouponCode\":\"{couponCode}\",\"DiscountAmount\":{discountAmount}}}";
            return new(orderId, null, OrderStatus.New, $"Discount applied: {couponCode} ({discountAmount})", createdById,
                       OrderHistoryEventTypes.DiscountApplied, metadata);
        }

        public static OrderHistory CouponUsed(long orderId, string couponCode, long createdById)
        {
            var metadata = $"{{\"CouponCode\":\"{couponCode}\"}}";
            return new(orderId, null, OrderStatus.New, $"Coupon used: {couponCode}", createdById,
                       OrderHistoryEventTypes.CouponUsed, metadata);
        }

        public static OrderHistory ItemAdded(long orderId, long productId, int quantity, long createdById)
        {
            var metadata = $"{{\"ProductId\":{productId},\"Quantity\":{quantity}}}";
            return new OrderHistory(orderId, null, OrderStatus.New,
                                    $"Item added: ProductId={productId}, Quantity={quantity}",
                                    createdById, "ItemAdded", metadata);
        }

        public static OrderHistory ItemRemoved(long orderId, long orderItemId, long createdById)
        {
            var metadata = $"{{\"OrderItemId\":{orderItemId}}}";
            return new OrderHistory(orderId, null, OrderStatus.New,
                                    $"Item removed: OrderItemId={orderItemId}",
                                    createdById, "ItemRemoved", metadata);
        }

        public static OrderHistory ChildOrderAdded(long orderId, long childOrderId, long createdById)
        {
            var metadata = $"{{\"ChildOrderId\":{childOrderId}}}";
            return new OrderHistory(orderId, null, OrderStatus.New,
                                    $"Child order added: ChildOrderId={childOrderId}",
                                    createdById, "ChildOrderAdded", metadata);
        }

    }

    public static class OrderHistoryEventTypes
    {
        public const string StatusChanged = "StatusChanged";
        public const string DiscountApplied = "DiscountApplied";
        public const string CouponUsed = "CouponUsed";
        // سایر رویدادهای محتمل مثل PaymentCaptured, ShipmentCreated هم می‌توان اضافه کرد
    }
}
