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
    public class UpdateProductCategoryCommandHandler
        : IAppRequestHandler<UpdateProductCategoryCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateProductCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(
            UpdateProductCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ProductCategory>();

            var category = await repository.GetAsync(
                predicate: x => x.Id == request.Id
                             && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Translations)
                               .Include(c => c.Children)
                               .Include(c => c.MediaAttachments)
                               );

            if (category is null)
                return ResultModel.Fail("Product category not found.");

            try
            {
                UpdateTranslation(category, request);
                await UpdateParentAsync(category, request, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel.Success();
            }
            catch (DomainException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                // در پروژه واقعی: لاگ کن
                return ResultModel.Fail("An unexpected error occurred: " + ex.Message);
            }
        }

        private static void UpdateTranslation(ProductCategory category, UpdateProductCategoryCommand request)
        {
            // درست: دیگه Id نمی‌فرستیم!
            category.AddOrUpdateTranslation(
                languageId: request.WebSiteLanguageId,
                title: request.Title,
                description: request.Description,
                slug: request.Slug,
                metaDescription: request.MetaDescription,
                metaTitle: request.MetaTitle,
                metaKeywords: request.MetaKeywords,
                canonicalUrl: request.CanonicalUrl
            );
        }

        private async Task UpdateParentAsync(
            ProductCategory category,
            UpdateProductCategoryCommand request,
            CancellationToken cancellationToken)
        {
            // اول والد قبلی رو پاک کن
            category.ClearParent();

            if (request.ParentId.HasValue)
            {
                if (request.ParentId.Value == category.Id)
                    throw new DomainException("A category cannot be its own parent.");

                var parentRepo = _unitOfWork.GetRepository<ProductCategory>();
                var parent = await parentRepo.GetByIdAsync(
                    id: request.ParentId.Value
                );

                if (parent == null)
                    throw new DomainException("Parent category not found.");

                if (parent.WebSiteId != _tenantContext.TenantId)
                    throw new DomainException("Parent category belongs to another tenant.");

                // چک چرخه (Cycle Detection) - خیلی مهمه!
                if (IsDescendantOf(category, parent))
                    throw new DomainException("Cannot set parent: it would create a circular reference.");

                category.SetParent(parent);
            }
        }

        // متد کمکی برای تشخیص چرخه
        private static bool IsDescendantOf(ProductCategory child, ProductCategory potentialAncestor)
        {
            var current = child.Parent;
            while (current != null)
            {
                if (current.Id == potentialAncestor.Id)
                    return true;
                current = current.Parent;
            }
            return false;
        }
    }
}