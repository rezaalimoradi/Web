using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Pages.Dtos;
using CMS.Application.Pages.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Pages.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Pages.QueryHandlers
{
    public class GetAllPagesQueryHandler : IAppRequestHandler<GetAllPagesQuery, ResultModel<List<PageDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllPagesQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<PageDto>>> Handle(GetAllPagesQuery request, CancellationToken cancellationToken)
        {
            var websiteId = _tenantContext.TenantId;
            var languageId = _tenantContext.CurrentLanguageId;

            var result = await _unitOfWork.GetRepository<Page>().GetAllPagedAsync(
                                                                predicate: x => x.WebSiteId == _tenantContext.TenantId,
                                                                func: x => x.Include(y => y.Translations),
                                                                pageIndex: request.Page,
                                                                pageSize: request.PageSize);

            var dtos = result.Select(page =>
            {
                var translation = page.Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);

                return new PageDto
                {
                    Id = page.Id,
                    Status = page.Status,
                    PublishAt = page.PublishAt,
                    ShowInMenu = page.ShowInMenu,
                    Translations = translation is null
                        ? new List<PageTranslationDto>()
                        : new List<PageTranslationDto>
                        {
                            new PageTranslationDto
                            {
                                Id = translation.Id,
                                WebSiteLanguageId = translation.WebSiteLanguageId,
                                Title = translation.Title,
                                Slug = translation.Slug,
                                Content = translation.Content,
                                SeoTitle = translation.SeoTitle,
                                SeoDescription = translation.SeoDescription,
                                MetaKeywords = translation.MetaKeywords,
                                CanonicalUrl = translation.CanonicalUrl
                            }
                        }

                };
            }).ToList();

            return ResultModel<List<PageDto>>.Success(dtos);
        }
    }
}
