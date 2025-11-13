using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Pages.Entities;

namespace CMS.Application.Pages.Queries
{
    public class GetPageByIdQuery : IAppRequest<ResultModel<Page?>>
    {
        public long Id { get; set; }
        public long WebSiteLanguageId { get; set; }
    }
}
