using CMS.Application.Catalog.Commands;
using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class UpdateTaxTranslationCommandHandler : IAppRequestHandler<UpdateTaxTranslationCommand, ResultModel<TaxTranslationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaxTranslationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<TaxTranslationDto>> Handle(UpdateTaxTranslationCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Tax>();
            var tax = await repository.GetAsync(
                predicate: x => x.Translations.Any(t => t.Id == request.Id),
                include: x => x.Include(t => t.Translations)
            );

            if (tax == null)
                return ResultModel<TaxTranslationDto>.Fail("Tax translation not found.");

            try
            {
                tax.UpdateTranslation(request.Id, request.Name, request.Description);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var translation = tax.Translations.First(t => t.Id == request.Id);
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
