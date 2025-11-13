using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Menus.Commands
{
    public class CreateMenuCommand : IAppRequest<ResultModel<long>>
    {
        public string Name { get; set; }
    }
}
