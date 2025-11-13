using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Pages.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Pages.Entities;

namespace CMS.Application.Pages.CommandHandlers
{
    public class CreatePageCommandHandler : IAppRequestHandler<CreatePageCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreatePageCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {

            var page = new Page(
                websiteId: _tenantContext.TenantId,
                status: request.Status,
                publishAt: request.PublishAt,
                showInMenu: request.ShowInMenu
            );

            try
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

                await _unitOfWork.GetRepository<Page>().InsertAsync(page);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(page.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
