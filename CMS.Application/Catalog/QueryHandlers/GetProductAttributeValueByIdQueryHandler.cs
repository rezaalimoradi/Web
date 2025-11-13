using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetProductAttributeValueByIdQueryHandler
            : IAppRequestHandler<GetProductAttributeValueByIdQuery, ResultModel<ProductAttributeValue>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductAttributeValueByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<ProductAttributeValue>> Handle(GetProductAttributeValueByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<ProductAttributeValue>();

            var value = await repo.Table
                .Include(v => v.Translations)
                .Include(v => v.ProductAttribute)
                .Where(v => v.Id == request.Id /*&& v.ProductAttribute. == _tenantContext.TenantId*/)
                .FirstOrDefaultAsync(cancellationToken);

            if (value == null)
                return ResultModel<ProductAttributeValue>.Fail("Attribute value not found.");

            return ResultModel<ProductAttributeValue>.Success(value);
        }
    }
}
