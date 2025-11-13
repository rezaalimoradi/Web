using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class DeleteProductCommandHandler : IAppRequestHandler<DeleteProductCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Product>();

            var product = await repository.GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(p => p.Translations));

            if (product == null)
            {
                return ResultModel.Fail("Product not found.");
            }

            repository.Delete(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel.Success();
        }
    }
}
