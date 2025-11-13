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
    public class GetCartItemTranslationByIdQueryHandler : IAppRequestHandler<GetCartItemTranslationByIdQuery, ResultModel<CartItemTranslationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetCartItemTranslationByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<CartItemTranslationDto>> Handle(GetCartItemTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CartItemTranslation>();

            var translation = await repo.Table
                .Where(t => t.Id == request.Id && t.CartItem.Cart.WebSiteId == _tenantContext.TenantId)
                .Select(t => new CartItemTranslationDto
                {
                    Id = t.Id,
                    CartItemId = t.CartItemId,
                    LanguageId = t.WebSiteLanguageId,
                    Title = t.Title,
                    CartId = t.CartItem.CartId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (translation == null)
                return ResultModel<CartItemTranslationDto>.Fail("Cart item translation not found");

            return ResultModel<CartItemTranslationDto>.Success(translation);
        }
    }
}
