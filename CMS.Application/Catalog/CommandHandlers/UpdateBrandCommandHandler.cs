using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Localization.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers;

internal class UpdateBrandCommandHandler : IAppRequestHandler<UpdateBrandCommand, ResultModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantContext _tenantContext;

    public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
    {
        _unitOfWork = unitOfWork;
        _tenantContext = tenantContext;
    }

    public async Task<ResultModel> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<Brand>();

        var brand = await repository.GetAsync(
            predicate: b => b.Id == request.Id && b.WebSiteId == _tenantContext.TenantId,
            include: b => b.Include(b => b.Translations));

        if (brand == null)
            return ResultModel.Fail("Brand not found");

        try
        {
            brand.AddOrUpdateTranslation(
                request.Id,
                request.LanguageId,
                request.Title,
                request.Description,
                request.Slug,
                request.MetaDescription,
                request.MetaTitle,
                request.MetaKeywords,
                request.CanonicalUrl
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel.Success();
        }
        catch (DomainException ex)
        {
            return ResultModel.Fail(ex.Message);
        }
    }
}
