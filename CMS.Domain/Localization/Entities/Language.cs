using CMS.Domain.Common;

namespace CMS.Domain.Localization.Entities
{
    public partial class Language : BaseEntity
    {
        public string  Code { get; set; }

        public string Name { get; set; }

        public Language() { }

        public Language(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
