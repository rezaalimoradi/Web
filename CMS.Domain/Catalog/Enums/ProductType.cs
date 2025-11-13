namespace CMS.Domain.Catalog.Enums
{
    public enum ProductType
    {
        Simple = 0,       // محصول ساده
        Variable = 1,     // محصول ترکیبی با ویژگی‌ها (مثلاً سایز/رنگ)
        Downloadable = 2, // قابل دانلود
        Virtual = 3,      // مجازی (بدون حمل و نقل)
        Grouped = 4       // گروهی یا باندل‌شده
    }
}
