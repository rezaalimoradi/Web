using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.QueryHandlers
{
    public class GetCartTranslationByIdQueryHandler : IAppRequestHandler<GetCartTranslationByIdQuery, ResultModel<CartTranslation>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetCartTranslationByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<CartTranslation>> Handle(GetCartTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            var translation = await _unitOfWork.GetRepository<CartTranslation>()
                .GetAsync(
                    predicate: x => x.Id == request.Id && x.Cart.WebSiteId == _tenantContext.TenantId,
                    include: x => x.Include(t => t.Cart)
                                   .Include(t => t.WebSiteLanguage) // بارگذاری زبان ترجمه
                );

            if (translation is null)
                return ResultModel<CartTranslation>.Fail("Cart translation not found");

            return ResultModel<CartTranslation>.Success(translation);
        }
    }
}
