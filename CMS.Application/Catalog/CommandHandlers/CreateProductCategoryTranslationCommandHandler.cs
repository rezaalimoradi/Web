using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers // حتماً namespace درست باشه
{
    public class CreateProductCategoryTranslationCommandHandler
        : IAppRequestHandler<CreateProductCategoryTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateProductCategoryTranslationCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(
            CreateProductCategoryTranslationCommand request,
            CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ProductCategory>();

            var category = await repository.GetAsync(
                predicate: x => x.Id == request.ProductCategoryId
                              && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Translations)
            );

            if (category == null)
                return ResultModel<long>.Fail("Product category not found.");

            try
            {
                // درست: فقط languageId و محتوا
                category.AddTranslation(
                    languageId: request.WebSiteLanguageId,
                    title: request.Title,
                    description: request.Description,
                    slug: request.Slug,
                    metaTitle: request.MetaTitle,
                    metaDescription: request.MetaDescription,
                    metaKeywords: request.MetaKeywords,
                    canonicalUrl: request.CanonicalUrl
                );

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(category.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                // لاگ کردن در پروژه واقعی
                return ResultModel<long>.Fail("An error occurred while adding translation: " + ex.Message);
            }
        }
    }
}