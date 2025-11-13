using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Pages.Dtos;

namespace CMS.Application.Pages.Queries
{
    public class GetAllPagesQuery : IAppRequest<ResultModel<List<PageDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
