using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class DeleteTaxTranslationCommandHandler : IAppRequestHandler<DeleteTaxTranslationCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaxTranslationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<bool>> Handle(DeleteTaxTranslationCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Tax>();

            var tax = await repository.GetAsync(
                predicate: x => x.Translations.Any(t => t.Id == request.Id),
                include: x => x.Include(t => t.Translations)
            );

            if (tax == null)
                return ResultModel<bool>.Fail("Tax translation not found.");

            try
            {
                tax.RemoveTranslation(request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<bool>.Success(true);
            }
            catch (DomainException ex)
            {
                return ResultModel<bool>.Fail(ex.Message);
            }
        }
    }

}
