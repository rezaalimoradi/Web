using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.CommandHandlers;

internal class DeleteBrandCommandHandler : IAppRequestHandler<DeleteBrandCommand, ResultModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantContext _tenantContext;

    public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
    {
        _unitOfWork = unitOfWork;
        _tenantContext = tenantContext;
    }

    public async Task<ResultModel> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<Brand>();

        var brand = await repository.GetAsync(
            b => b.Id == request.Id && b.WebSiteId == _tenantContext.TenantId);

        if (brand == null)
            return ResultModel.Fail("Brand not found");

        repository.Delete(brand);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultModel.Success();
    }
}
