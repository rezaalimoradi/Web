using CMS.Application.Catalog.Commands;
using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers;

internal class UpdateBrandTranslationCommandHandler : IAppRequestHandler<UpdateBrandTranslationCommand, ResultModel<BrandTranslationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantContext _tenantContext;

    public UpdateBrandTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
    {
        _unitOfWork = unitOfWork;
        _tenantContext = tenantContext;
    }

    public async Task<ResultModel<BrandTranslationDto>> Handle(UpdateBrandTranslationCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<BrandTranslation>();

        var translation = await repository.GetAsync(
            predicate: t => t.Id == request.Id && t.Brand.WebSiteId == _tenantContext.TenantId,
            include: t => t.Include(x => x.Brand));

        if (translation == null)
            return ResultModel<BrandTranslationDto>.Fail("Translation not found");

        translation.Update(
            request.Title,
            request.Description,
            request.Slug,
            request.MetaTitle,
            request.MetaDescription,
            request.MetaKeywords,
            request.CanonicalUrl
        );

        repository.Update(translation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = new BrandTranslationDto
        {
            Id = translation.Id,
            BrandId = translation.BrandId,
            LanguageId = translation.WebSiteLanguageId,
            Title = translation.Title,
            Slug = translation.Slug,
            Description = translation.Description
        };

        return ResultModel<BrandTranslationDto>.Success(dto);
    }
}
