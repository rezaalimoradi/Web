using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Pages.Enums;

namespace CMS.Application.Pages.Commands
{
    public class UpdatePageCommand : IAppRequest<ResultModel>
    {
        public long Id { get; set; } // PageId
        public long TranslationId { get; set; } // اینو اضافه کن
        public long WebSiteLanguageId { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string CanonicalUrl { get; set; }

        public PageStatus Status { get; set; }
        public DateTime? PublishAt { get; set; }
        public bool ShowInMenu { get; set; }
    }
}
