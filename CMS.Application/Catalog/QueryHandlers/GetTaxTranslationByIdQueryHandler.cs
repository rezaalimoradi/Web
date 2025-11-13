using CMS.Application.Catalog.Dtos;
using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetTaxTranslationByIdQueryHandler
        : IAppRequestHandler<GetTaxTranslationByIdQuery, ResultModel<TaxTranslationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTaxTranslationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<TaxTranslationDto>> Handle(
            GetTaxTranslationByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GetRepository<TaxTranslation>().GetAsync(predicate: x => x.Id == request.Id);

            if (entity == null)
                return ResultModel<TaxTranslationDto>.Fail("Tax translation not found.");

            var translation = await _unitOfWork
                .GetRepository<TaxTranslation>()
                .GetAsync(x => x.Id == request.Id);

            if (translation == null)
                return ResultModel<TaxTranslationDto>.Fail("Tax translation not found.");

            return ResultModel<TaxTranslationDto>.Success(new TaxTranslationDto
            {
                Id = translation.Id,
                TaxId = translation.TaxId,
                LanguageId = translation.WebSiteLanguageId,
                Name = translation.Name,
                Description = translation.Description
            });

        }
    }
}
