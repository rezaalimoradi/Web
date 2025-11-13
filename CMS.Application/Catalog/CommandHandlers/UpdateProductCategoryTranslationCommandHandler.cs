using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class UpdateProductCategoryTranslationCommandHandler : IAppRequestHandler<UpdateProductCategoryTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateProductCategoryTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdateProductCategoryTranslationCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ProductCategoryTranslation>();

            var translation = await repository.GetAsync(
                predicate: x => x.Id == request.Id && x.ProductCategory.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(t => t.ProductCategory));

            if (translation == null)
                return ResultModel.Fail("Product category translation not found.");

            try
            {
                // آپدیت اصلی
                translation.Update(
                    title: request.Title,
                    description: request.Description,
                    slug: request.Slug
                );

                // آپدیت metadata
                translation.UpdateMeta(
                    metaTitle: request.MetaTitle,
                    metaDescription: request.MetaDescription,
                    metaKeywords: request.MetaKeywords,
                    canonicalUrl: request.CanonicalUrl
                );

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
