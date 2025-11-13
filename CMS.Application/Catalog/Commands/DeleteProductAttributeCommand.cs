using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class DeleteProductAttributeCommand : IAppRequest<ResultModel<bool>>
    {
        public long id { get; set; }
    }
}
