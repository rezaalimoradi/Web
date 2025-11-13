using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Pages.Commands
{
    public class DeletePageCommand : IAppRequest<ResultModel>
    {
        public long Id { get; set; }
    }
}
