using CMS.Application.Media;
using CMS.Infrastructure.Media.Factory;
using Microsoft.AspNetCore.Http;

namespace CMS.Infrastructure.Media.Services
{
    public class MediaStorageService : IMediaStorageService
    {
        private readonly MediaStorageFactory _factory;

        public MediaStorageService(MediaStorageFactory factory)
        {
            _factory = factory;
        }

        public async Task<string> UploadAsync(IFormFile file, string container)
        {
            var strategy = _factory.CreateStrategy();
            return await strategy.UploadAsync(file, container);
        }

        public async Task DeleteAsync(string key)
        {
            var strategy = _factory.CreateStrategy();
            await strategy.DeleteAsync(key);
        }

        public string GetUrl(string key)
        {
            var strategy = _factory.CreateStrategy();
            return strategy.GetUrl(key);
        }
    }
}
