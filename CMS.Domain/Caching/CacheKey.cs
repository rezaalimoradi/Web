namespace CMS.Domain.Caching
{
    public partial class CacheKey
    {
        public CacheKey(string key)
        {
            Key = key;
        }

        public string Key { get; protected set; }

        public int CacheTime { get; set; } = 30;
    }
}
