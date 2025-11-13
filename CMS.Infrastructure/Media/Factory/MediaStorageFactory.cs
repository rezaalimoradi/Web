using CMS.Application.Media;
using CMS.Infrastructure.Media.Strategies;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Media.Factory
{
    public class MediaStorageFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public MediaStorageFactory(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public IMediaStorageStrategy CreateStrategy()
        {
            var storageType = _configuration["Media:StorageType"]?.ToLower() ?? "local";

            return storageType switch
            {
                "local" => new LocalMediaStorageStrategy(
                    _configuration["Media:Local:RootPath"]!,
                    _configuration["Media:Local:BaseUrl"]!),
                // در آینده:
                // "azure" => _serviceProvider.GetRequiredService<AzureMediaStorageStrategy>(),
                // "s3" => _serviceProvider.GetRequiredService<S3MediaStorageStrategy>(),
                _ => throw new InvalidOperationException($"Unsupported media storage type: {storageType}")
            };
        }
    }
}
