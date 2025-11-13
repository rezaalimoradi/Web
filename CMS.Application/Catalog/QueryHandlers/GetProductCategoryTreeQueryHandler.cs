// File: GetProductCategoryTreeQueryHandler.cs
using CMS.Application.Catalog.Dtos;
using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetProductCategoryTreeQueryHandler
        : IAppRequestHandler<GetProductCategoryTreeQuery, ResultModel<List<ProductCategoryTreeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductCategoryTreeQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<ProductCategoryTreeDto>>> Handle(
            GetProductCategoryTreeQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var languageId = request.CurrentLanguageId ?? _tenantContext.CurrentLanguageId;

                var categories = await _unitOfWork.GetRepository<ProductCategory>()
                    .GetAllAsync(
                        predicate: x => x.WebSiteId == _tenantContext.TenantId,
                        func: x => x
                            .Include(c => c.Translations)
                            .Include(c => c.MediaAttachments)
                                .ThenInclude(ma => ma.MediaFile)
                            .Include(c => c.Children)
                    );

                var rootCategories = categories
                    .Where(c => c.ParentId == null)
                    .OrderBy(c => c.Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)?.Title)
                    .ToList();

                var lookup = categories.ToDictionary(c => c.Id);
                var tree = BuildTree(rootCategories, lookup, languageId);

                return ResultModel<List<ProductCategoryTreeDto>>.Success(tree);
            }
            catch (Exception ex)
            {
                return ResultModel<List<ProductCategoryTreeDto>>.Fail("خطا در بارگذاری درخت دسته‌بندی: " + ex.Message);
            }
        }

        private List<ProductCategoryTreeDto> BuildTree(
            List<ProductCategory> roots,
            Dictionary<long, ProductCategory> lookup,
            long languageId)
        {
            var result = new List<ProductCategoryTreeDto>();

            foreach (var category in roots)
            {
                var dto = MapToDto(category, lookup, languageId, 0);
                result.Add(dto);
            }

            return result;
        }

        private ProductCategoryTreeDto MapToDto(
            ProductCategory category,
            Dictionary<long, ProductCategory> lookup,
            long languageId,
            int depth)
        {
            var translation = category.Translations
                .FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                ?? category.Translations.FirstOrDefault();

            var mainImageKey = category.MainImage?.MediaFile?.Key;

            var dto = new ProductCategoryTreeDto
            {
                Id = category.Id,
                Title = translation?.Title ?? $"دسته‌بندی {category.Id}",
                Slug = translation?.Slug ?? "",
                Depth = depth,
                HasChildren = category.Children.Any(),
                ImageUrl = !string.IsNullOrEmpty(mainImageKey) ? $"/uploads/{mainImageKey}" : null,
                Value = category.Id,
                Label = $"{new string('─', depth)} {translation?.Title ?? "بدون عنوان"}".Trim(),
                Children = new List<ProductCategoryTreeDto>() // مهم: لیست خالی بساز
            };

            // مهم: فرزندها رو اضافه کن!
            foreach (var child in category.Children
                .OrderBy(c => c.Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)?.Title ?? ""))
            {
                if (lookup.TryGetValue(child.Id, out var childEntity))
                {
                    dto.Children.Add(MapToDto(childEntity, lookup, languageId, depth + 1));
                }
            }

            return dto;
        }
    }
}