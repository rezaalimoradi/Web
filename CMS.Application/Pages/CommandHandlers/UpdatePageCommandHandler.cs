using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Pages.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Pages.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Pages.CommandHandlers
{
    public class UpdatePageCommandHandler : IAppRequestHandler<UpdatePageCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdatePageCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Page>();
            var page = await _unitOfWork.GetRepository<Page>().GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(p => p.Translations));

            if (page == null || page.WebSiteId != _tenantContext.TenantId)
                return ResultModel.Fail("Page not found.");

            try
            {
                page.UpdateStatus(request.Status);
                page.UpdatePublishDate(request.PublishAt);
                page.UpdateShowInMenu(request.ShowInMenu);

                var translation = page.Translations.FirstOrDefault(x => x.WebSiteLanguageId == request.WebSiteLanguageId);
                if (translation == null)
                {
                    page.AddTranslation(
                        languageId: request.WebSiteLanguageId,
                        title: request.Title,
                        slug: request.Slug,
                        content: request.Content,
                        seoTitle: request.SeoTitle,
                        seoDescription: request.SeoDescription,
                        metaKeywords: request.MetaKeywords,
                        canonicalUrl: request.CanonicalUrl
                    );
                }
                else
                {
                    translation.Update(
                        title: request.Title,
                        slug: request.Slug,
                        content: request.Content,
                        seoTitle: request.SeoTitle,
                        seoDescription: request.SeoDescription,
                        metaKeywords: request.MetaKeywords,
                        canonicalUrl: request.CanonicalUrl
                    );
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResultModel.Success();
            }
            catch (DomainException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
