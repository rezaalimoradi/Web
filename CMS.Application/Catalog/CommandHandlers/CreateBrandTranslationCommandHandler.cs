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
    internal class CreateBrandTranslationCommandHandler : IAppRequestHandler<CreateBrandTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateBrandTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateBrandTranslationCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Brand>();

            var brand = await repository.GetAsync(
                predicate: b => b.Id == request.BrandId && b.WebSiteId == _tenantContext.TenantId,
                include: b => b.Include(b => b.Translations)
            );

            if (brand == null)
                return ResultModel<long>.Fail("Brand not found.");

            try
            {
                brand.AddTranslation(
                    request.BrandId,
                    request.LanguageId,
                    request.Title,
                    request.Description,
                    request.Slug,
                    request.MetaKeywords,
                    request.MetaDescription,
                    request.MetaTitle,
                    request.CanonicalUrl
                );

                repository.Update(brand);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(brand.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }

}
