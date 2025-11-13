using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Application.Orders.Carts.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.QueryHandlers
{
    public class GetAllCartsQueryHandler
        : IAppRequestHandler<GetAllCartsQuery, ResultModel<List<CartDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllCartsQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<CartDto>>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();

            var carts = await repo.Table
                .Where(c => c.WebSiteId == _tenantContext.TenantId)
                .Select(c => new CartDto
                {
                    Id = c.Id,
                    CustomerIdentifier = c.CustomerIdentifier,
                    Items = c.Items.Select(i => new CartItemDto
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        Discount = i.Discount,
                        // ProductName فقط اگر Product navigation وجود داشته باشد
                        ProductName = null
                    }).ToList(),
                    Translations = c.Translations.Select(t => new CartTranslationDto
                    {
                        Id = t.Id,
                        LanguageId = t.WebSiteLanguageId,
                        Title = t.Title,
                        Description = t.Description,
                        LanguageName = null // فقط اگر WebSiteLanguage -> Language mapping درست شود
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return ResultModel<List<CartDto>>.Success(carts);
        }

    }
}
