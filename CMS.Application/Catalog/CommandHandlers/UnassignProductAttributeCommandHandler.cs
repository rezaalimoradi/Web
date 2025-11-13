using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class UnassignProductAttributeCommandHandler
        : IAppRequestHandler<UnassignProductAttributeCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnassignProductAttributeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<bool>> Handle(UnassignProductAttributeCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<ProductProductAttribute>();

            var entity = await repo.GetAsync(
                predicate: x => x.ProductId == request.ProductId && x.ProductAttributeId == request.AttributeId,
                include: q => q.Include(x => x.ValueMappings)
            );

            if (entity == null)
                return ResultModel<bool>.Fail("این ویژگی برای محصول وجود ندارد.");

            foreach (var mapping in entity.ValueMappings.ToList())
                _unitOfWork.GetRepository<ProductProductAttribute_ValueMapping>().Delete(mapping);

            repo.Delete(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return ResultModel<bool>.Success(true);
        }
    }
}
