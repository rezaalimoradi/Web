using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Application.Orders.Carts.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.QueryHandlers
{
    public class GetAllCartTranslationsQueryHandler : IAppRequestHandler<GetAllCartTranslationsQuery, ResultModel<List<CartTranslationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllCartTranslationsQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<CartTranslationDto>>> Handle(GetAllCartTranslationsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CartTranslation>();

            var translations = await repo.Table
                .Where(x => x.Cart.WebSiteId == _tenantContext.TenantId)
                .Select(t => new CartTranslationDto
                {
                    Id = t.Id,
                    CartId = t.CartId,
                    LanguageId = t.WebSiteLanguageId,
                    Title = t.Title,
                    Description = t.Description,
                    LanguageName = t.WebSiteLanguage.Language.Name
                })
                .ToListAsync(cancellationToken);

            return ResultModel<List<CartTranslationDto>>.Success(translations);
        }

    }
}
