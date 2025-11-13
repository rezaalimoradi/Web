using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Media
{
    public interface IMediaStorageService
    {
        Task<string> UploadAsync(IFormFile file, string container);
        Task DeleteAsync(string key);
        string GetUrl(string key);
    }
}
