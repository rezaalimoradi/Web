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
    public class GetAllCartItemTranslationsQueryHandler
        : IAppRequestHandler<GetAllCartItemTranslationsQuery, ResultModel<List<CartItemTranslationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllCartItemTranslationsQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<CartItemTranslationDto>>> Handle(GetAllCartItemTranslationsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CartItemTranslation>();

            var translations = await repo.Table
                .Where(x => x.CartItem.Cart.WebSiteId == _tenantContext.TenantId &&
                            x.CartItemId == request.CartItemId)
                .Select(t => new CartItemTranslationDto
                {
                    Id = t.Id,
                    CartItemId = t.CartItemId,
                    LanguageId = t.WebSiteLanguageId,
                    Title = t.Title,
                    LanguageName = t.WebSiteLanguage.Language.Name
                })
                .ToListAsync(cancellationToken);

            return ResultModel<List<CartItemTranslationDto>>.Success(translations);
        }
    }
}
