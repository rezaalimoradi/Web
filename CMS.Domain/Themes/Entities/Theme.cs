using CMS.Domain.Common;

namespace CMS.Domain.Themes.Entities
{
    public class Theme : AggregateRoot
    {
        public string Name { get; set; }

        public Theme() { }

        public Theme(string name)
        {
            Name = name;
        }
    }
}
