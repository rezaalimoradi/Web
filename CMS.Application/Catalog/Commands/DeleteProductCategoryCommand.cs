using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class DeleteProductCategoryCommand : IAppRequest<ResultModel>
    {
        public long Id { get; set; }
    }
}
