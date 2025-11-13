using CMS.Application.CompareItems.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.CompareItems.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.CompareItems.CommandHandlers
{
    internal class AddToCompareCommandHandler : IAppRequestHandler<AddToCompareCommand, ResultModel<bool>>
    {
        private readonly IRepository<CompareList> _compareListRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AddToCompareCommandHandler(IRepository<CompareList> compareListRepo, IUnitOfWork unitOfWork)
        {
            _compareListRepo = compareListRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<bool>> Handle(AddToCompareCommand request, CancellationToken cancellationToken)
        {
            var compareList = await _compareListRepo.Table
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId && x.WebsiteId == request.WebsiteId, cancellationToken);

            if (compareList == null)
            {
                compareList = new CompareList(request.WebsiteId, request.CustomerId);
                await _compareListRepo.InsertAsync(compareList);
            }

            if (!compareList.Items.Any(i => i.ProductId == request.ProductId))
                compareList.Items.Add(new CompareItem(compareList.Id, request.ProductId));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel<bool>.Success(true);
        }

    }
}
