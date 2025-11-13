using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Medias.Dtos
{
    public class MediaFileDto
    {
        public long Id { get; set; }
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public long SizeInBytes { get; set; }
        public string MediaType { get; set; } = default!;
        public string Url { get; set; } = default!;
        public DateTime UploadedAt { get; set; }
    }
}
