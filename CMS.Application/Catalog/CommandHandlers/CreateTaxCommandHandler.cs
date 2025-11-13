using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class CreateTaxCommandHandler : IAppRequestHandler<CreateTaxCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateTaxCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tax = new Tax(_tenantContext.TenantId, request.Rate);

                if (!request.IsActive)
                    tax.Deactivate();

                tax.AddTranslation(
                    request.WebSiteLanguageId,
                    request.Name ?? throw new ArgumentNullException(nameof(request.Name)),
                    request.Description
                );

                await _unitOfWork.GetRepository<Tax>().InsertAsync(tax);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(tax.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }

}
