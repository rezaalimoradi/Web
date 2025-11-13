namespace CMS.Application.Models.Common
{
    public class LocalizationSettings
    {
        public string DefaultCulture { get; set; }

        public List<string> SupportedCultures { get; set; } = [];

        public Dictionary<string, string> CultureMap { get; set; }
    }
}
