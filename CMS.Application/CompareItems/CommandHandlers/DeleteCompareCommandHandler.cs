using CMS.Application.CompareItems.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Common;
using CMS.Domain.CompareItems.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.CompareItems.CommandHandlers
{
    internal class DeleteCompareItemCommandHandler : IAppRequestHandler<DeleteCompareItemCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCompareItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<bool>> Handle(DeleteCompareItemCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CompareList>();

            var compareList = await repo.Table
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x =>
                    x.CustomerId == request.CustomerId &&
                    x.WebsiteId == request.WebsiteId,
                    cancellationToken);

            if (compareList == null)
                return ResultModel<bool>.Fail("لیست مقایسه پیدا نشد.");

            var item = compareList.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item == null)
                return ResultModel<bool>.Fail("محصول در لیست مقایسه یافت نشد.");

            compareList.Items.Remove(item);

            if (!compareList.Items.Any())
                _unitOfWork.GetRepository<CompareList>().Delete(compareList);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel<bool>.Success(true);
        }
    }
}
