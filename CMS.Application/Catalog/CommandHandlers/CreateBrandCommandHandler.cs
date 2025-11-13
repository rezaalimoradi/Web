using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class CreateBrandCommandHandler : IAppRequestHandler<CreateBrandCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = Brand.Create(_tenantContext.TenantId);

            try
            {
                brand.AddTranslation(
                    brand.Id,
                    request.WebSiteLanguageId,
                    request.Title,
                    request.Description,
                    request.Slug,
                    request.MetaDescription,
                    request.MetaTitle,
                    request.MetaKeywords,
                    request.CanonicalUrl
                );

                await _unitOfWork.GetRepository<Brand>().InsertAsync(brand);
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
