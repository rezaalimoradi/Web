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
    public class GetProductCategoriesByLanguageQueryHandler : IAppRequestHandler<GetProductCategoriesByLanguageQuery, ResultModel<List<ProductCategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductCategoriesByLanguageQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<ProductCategoryDto>>> Handle(
            GetProductCategoriesByLanguageQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ProductCategory>();

            var categories = await repository.GetAllAsync(
                predicate: x => x.WebSiteId == _tenantContext.TenantId,
                func: x => x.Include(y => y.Translations));

            var result = categories
                .Select(x =>
                {
                    var translation = x.Translations.FirstOrDefault(t => t.WebSiteLanguageId == request.LanguageId);
                    if (translation == null)
                        return null;

                    return new ProductCategoryDto
                    {
                        Id = x.Id,
                        Title = translation.Title,
                        Description = translation.Description,
                        Slug = translation.Slug
                    };
                })
                .Where(x => x != null)
                .ToList();

            return ResultModel<List<ProductCategoryDto>>.Success(result);
        }

    }
}
