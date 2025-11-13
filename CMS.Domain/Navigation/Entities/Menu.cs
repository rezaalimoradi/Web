using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Navigation.Entities
{
    public class Menu : AggregateRoot
    {
        public string Name { get; private set; }

        public long WebSiteId { get; private set; }

        public WebSite WebSite { get; set; }

        public ICollection<MenuItem> Items { get; private set; }

        private Menu()
        {
            Items = new List<MenuItem>();
        }

        public Menu(long webSiteId, string name)
        {
            WebSiteId = webSiteId;
            Name = name;
            Items = new List<MenuItem>();
        }

        public void AddItem(MenuItem menuItem)
        {
            if (menuItem == null)
                throw new DomainException("Invalid value");

            Items.Add(menuItem);
        }

        public void RemoveItem(MenuItem item)
        {
            if (!Items.Contains(item))
                throw new DomainException("Menu item not found in this menu.");

            Items.Remove(item);
        }
    }
}
