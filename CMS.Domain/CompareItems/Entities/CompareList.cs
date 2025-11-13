using CMS.Domain.Common;
using CMS.Domain.CompareItems.Entities;

public class CompareList : AggregateRoot
{
    public long WebsiteId { get; private set; }
    public long CustomerId { get; private set; }

    public List<CompareItem> Items { get; private set; } = new();

    protected CompareList() { }

    public CompareList(long websiteId, long customerId)
    {
        WebsiteId = websiteId;
        CustomerId = customerId;
    }

    public void AddItem(long productId)
    {
        if (!Items.Any(i => i.ProductId == productId))
            Items.Add(new CompareItem(this.Id, productId));
    }

    public void RemoveItem(long productId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
            Items.Remove(item);
    }
}
