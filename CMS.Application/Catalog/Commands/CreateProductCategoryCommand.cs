using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using Microsoft.AspNetCore.Http;

namespace CMS.Application.Catalog.Commands
{
    public class CreateProductCategoryCommand : IAppRequest<ResultModel<long>>
    {
        public long WebSiteLanguageId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public string? CanonicalUrl { get; set; }
        public long? ParentId { get; set; }

        public List<IFormFile>? ImageFiles { get; set; }
    }
}
