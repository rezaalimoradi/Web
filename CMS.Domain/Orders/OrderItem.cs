using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders;

public class OrderItem : BaseEntity
{
    public long OrderId { get; private set; }
    public Order Order { get; private set; }

    public long ProductId { get; private set; }
    public Product Product { get; private set; }

    public decimal ProductPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal TaxPercent { get; private set; }

    public decimal Total => Math.Max(0, (ProductPrice * Quantity) - DiscountAmount + TaxAmount);

    protected OrderItem() { }

    internal OrderItem(long orderId, long productId, decimal productPrice, int quantity,
                       decimal discountAmount = 0, decimal taxAmount = 0, decimal taxPercent = 0)
    {
        OrderId = ValidateId(orderId, nameof(orderId));
        ProductId = ValidateId(productId, nameof(productId));
        ProductPrice = ValidateDecimal(productPrice, nameof(productPrice));
        Quantity = ValidateQuantity(quantity);
        DiscountAmount = ValidateDecimal(discountAmount, nameof(discountAmount));
        TaxAmount = ValidateDecimal(taxAmount, nameof(taxAmount));
        TaxPercent = ValidateDecimal(taxPercent, nameof(taxPercent));
    }

    // --- Behaviors ---
    public void UpdateQuantity(int newQuantity) => Quantity = ValidateQuantity(newQuantity);
    public void ApplyDiscount(decimal discountAmount) => DiscountAmount = ValidateDecimal(discountAmount, nameof(discountAmount));
    public void ApplyTax(decimal taxAmount, decimal taxPercent)
    {
        TaxAmount = ValidateDecimal(taxAmount, nameof(taxAmount));
        TaxPercent = ValidateDecimal(taxPercent, nameof(taxPercent));
    }

    private static long ValidateId(long id, string paramName)
    {
        if (id <= 0) throw new DomainException($"{paramName} must be greater than zero.");
        return id;
    }
    private static decimal ValidateDecimal(decimal value, string paramName)
    {
        if (value < 0) throw new DomainException($"{paramName} cannot be negative.");
        return value;
    }
    private static int ValidateQuantity(int quantity)
    {
        if (quantity <= 0) throw new DomainException("Quantity must be greater than zero.");
        return quantity;
    }
}
