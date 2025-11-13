using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Medias.Commands
{
    public class DeleteMediaCommand : IAppRequest<ResultModel<bool>>
    {
        public long MediaId { get; set; }
    }
}
