using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class DeleteTaxCommandHandler : IAppRequestHandler<DeleteTaxCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaxCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<bool>> Handle(DeleteTaxCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Tax>();

            var tax = await repository.GetByIdAsync(request.Id);
            if (tax == null)
                return ResultModel<bool>.Fail("Tax not found.");

            try
            {
                repository.Delete(tax);
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
