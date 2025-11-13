using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Blog.Queries
{
    public class GetMenuByIdQuery : IAppRequest<ResultModel<Menu?>>
    {
        public long Id { get; set; }
    }
}
