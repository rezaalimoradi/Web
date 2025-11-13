using CMS.Application.Media;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Media.Strategies
{
    public class LocalMediaStorageStrategy : IMediaStorageStrategy
    {
        private readonly string _rootPath;
        private readonly string _baseUrl;

        public LocalMediaStorageStrategy(string rootPath, string baseUrl)
        {
            _rootPath = rootPath;
            _baseUrl = baseUrl.TrimEnd('/');
        }

        public async Task<string> UploadAsync(IFormFile file, string container)
        {
            var folderPath = Path.Combine(_rootPath, container);
            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Key = container/filename (برای ذخیره در DB)
            return $"{container}/{fileName}";
        }

        public async Task DeleteAsync(string key)
        {
            var fullPath = Path.Combine(_rootPath, key);
            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }

        public string GetUrl(string key)
        {
            return $"{_baseUrl}/{key.Replace("\\", "/")}";
        }
    }
}
