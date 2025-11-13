using CMS.Application.Catalog.Commands;
using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class UpdateTaxCommandHandler : IAppRequestHandler<UpdateTaxCommand, ResultModel<TaxDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaxCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<TaxDto>> Handle(UpdateTaxCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Tax>();
            var tax = await repository.GetByIdAsync(request.Id);

            if (tax == null)
                return ResultModel<TaxDto>.Fail("Tax not found.");

            try
            {
                tax.Update(request.Rate, request.IsActive); // متد Aggregate برای تغییرات
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var dto = new TaxDto
                {
                    Id = tax.Id,
                    Rate = tax.Rate,
                    IsActive = tax.IsActive,
                    WebSiteId = tax.WebSiteId
                };

                return ResultModel<TaxDto>.Success(dto);
            }
            catch (DomainException ex)
            {
                return ResultModel<TaxDto>.Fail(ex.Message);
            }
        }
    }

}
