using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers;

internal class DeleteBrandTranslationCommandHandler : IAppRequestHandler<DeleteBrandTranslationCommand, ResultModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantContext _tenantContext;

    public DeleteBrandTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
    {
        _unitOfWork = unitOfWork;
        _tenantContext = tenantContext;
    }

    public async Task<ResultModel> Handle(DeleteBrandTranslationCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<BrandTranslation>();

        var translation = await repository.GetAsync(
            t => t.Id == request.Id && t.Brand.WebSiteId == _tenantContext.TenantId,
            include: t => t.Include(x => x.Brand));

        if (translation == null)
            return ResultModel.Fail("Translation not found");

        repository.Delete(translation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultModel.Success("Translation deleted successfully");
    }
}
