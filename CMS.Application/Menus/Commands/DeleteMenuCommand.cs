using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Menus.Commands
{
    public class DeleteMenuCommand : IAppRequest<ResultModel<long>>
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
