using CMS.Application.Catalog.Commands;
using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class CreateTaxTranslationCommandHandler : IAppRequestHandler<CreateTaxTranslationCommand, ResultModel<TaxTranslationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateTaxTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<TaxTranslationDto>> Handle(CreateTaxTranslationCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Tax>();

            var tax = await repository.GetAsync(
                predicate: x => x.Id == request.TaxId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(t => t.Translations)
            );

            if (tax == null)
                return ResultModel<TaxTranslationDto>.Fail("Tax not found.");

            try
            {
                tax.AddTranslation(request.LanguageId, request.Name, request.Description);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var translation = tax.Translations.First(t => t.WebSiteLanguageId == request.LanguageId);

                var dto = new TaxTranslationDto
                {
                    Id = translation.Id,
                    TaxId = translation.TaxId,
                    LanguageId = translation.WebSiteLanguageId,
                    Name = translation.Name,
                    Description = translation.Description
                };

                return ResultModel<TaxTranslationDto>.Success(dto);
            }
            catch (DomainException ex)
            {
                return ResultModel<TaxTranslationDto>.Fail(ex.Message);
            }
        }
    }

}
