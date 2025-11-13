

using Microsoft.AspNetCore.Http;

namespace CMS.Application.Media
{
    public interface IMediaStorageStrategy
    {
        /// <summary>
        /// ذخیره فایل و برگرداندن key (مثلاً مسیر فایل یا شناسه در S3)
        /// </summary>
        Task<string> UploadAsync(IFormFile file, string container);

        /// <summary>
        /// حذف فایل از استور
        /// </summary>
        Task DeleteAsync(string key);

        /// <summary>
        /// تولید URL قابل دسترس برای نمایش
        /// </summary>
        string GetUrl(string key);
    }
}
