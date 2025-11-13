using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Medias.Dtos;
using CMS.Application.Models.Common;
using Microsoft.AspNetCore.Http;

namespace CMS.Application.Medias.Commands
{
    public class UploadMediaCommand : IAppRequest<ResultModel<MediaFileDto>>
    {
        public IFormFile File { get; set; } = default!;
        public string EntityType { get; set; } = default!;
        public long EntityId { get; set; }
        public string Purpose { get; set; } = "Main";
    }
}
