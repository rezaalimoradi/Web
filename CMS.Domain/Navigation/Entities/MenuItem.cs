using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Navigation.Entities
{
    public class MenuItem : BaseEntity
    {
        public long MenuId { get; private set; }
        public long? ParentId { get; set; }

        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }

        public Menu Menu { get; set; }
        public MenuItem Parent { get; set; }

        public ICollection<MenuItem> Children { get; private set; }
        public ICollection<MenuItemTranslation> Translations { get; private set; }

        private MenuItem() { }

        public MenuItem(long menuId, long? parentId, bool isActive, int displayOrder)
        {
            MenuId = menuId;
            ParentId = parentId;
            IsActive = isActive;
            DisplayOrder = displayOrder;
            Children = new List<MenuItem>();
            Translations = new List<MenuItemTranslation>();
        }

        public MenuItemTranslation AddTranslation(string title, string link, long webSiteLanguageId)
        {
            if (Translations.Any(x => x.WebSiteLanguageId == webSiteLanguageId))
                throw new DomainException("Translation for this language already exists.");

            var menuItemTranslation = new MenuItemTranslation(title, link, this.Id, webSiteLanguageId);

            Translations.Add(menuItemTranslation);

            return menuItemTranslation;
        }

        public void UpdateTranslation(long languageId, string title, string link)
        {
            var translation = Translations.FirstOrDefault(x => x.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found for the specified language.");

            translation.Title = title;
            translation.Link = link;
        }

        public void RemoveTranslation(MenuItemTranslation translation)
        {
            if (!Translations.Any(x => x.Id == translation.Id))
                throw new DomainException("Translation not found.");

            Translations.Remove(translation);
        }

        public void UpdateIsActive(bool isActive) => IsActive = isActive;
        public void UpdateDisplayOrder(int displayOrder) => DisplayOrder = displayOrder;

        public void SetParent(MenuItem parent)
        {
            if (parent == null)
                throw new DomainException("Parent cannot be null.");

            ParentId = parent.Id;
            Parent = parent;
            parent.Children.Add(this);
        }

        public void RemoveParent()
        {
            Parent?.Children.Remove(this);
            Parent = null;
            ParentId = null;
        }
    }
}
