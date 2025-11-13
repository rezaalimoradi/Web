using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Pricing
{
    public class CatalogRule : AggregateRoot
    {
        private readonly List<CatalogRuleTranslation> _translations = new();

        protected CatalogRule() { } // EF

        public CatalogRule(string name, bool isActive, DateTimeOffset? startOn, DateTimeOffset? endOn)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("CatalogRule name is required.");

            Name = name.Trim();
            IsActive = isActive;
            StartOn = startOn;
            EndOn = endOn;
            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        public DateTimeOffset? StartOn { get; private set; }
        public DateTimeOffset? EndOn { get; private set; }

        public IReadOnlyCollection<CatalogRuleTranslation> Translations => _translations.AsReadOnly();

        // --- Behaviors ---

        public void Update(string name, string description, DateTimeOffset? startOn, DateTimeOffset? endOn)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("CatalogRule name is required.");

            Name = name.Trim();
            Description = description?.Trim();
            StartOn = startOn;
            EndOn = endOn;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void AddTranslation(string culture, string name, string description)
        {
            if (_translations.Any(t => t.Culture == culture))
                throw new DomainException($"Translation for culture '{culture}' already exists.");

            _translations.Add(new CatalogRuleTranslation(Id, culture, name, description));
        }

        public void UpdateTranslation(string culture, string name, string description)
        {
            var translation = _translations.FirstOrDefault(t => t.Culture == culture);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(name, description);
        }

        public void RemoveTranslation(string culture)
        {
            var translation = _translations.FirstOrDefault(t => t.Culture == culture);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }

        public CatalogRuleTranslation GetTranslation(string culture) =>
            _translations.FirstOrDefault(t => t.Culture == culture);
    }
}
