using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class UpdateProductTranslationCommandHandler : IAppRequestHandler<UpdateProductTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateProductTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(UpdateProductTranslationCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetAsync(
                predicate: x => x.Id == request.ProductId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(p => p.Translations));

            if (product == null)
                return ResultModel<long>.Fail("Product not found.");

            try
            {
                product.UpdateTranslation(request.LanguageId, request.Name, request.Description, request.Slug);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResultModel<long>.Success(product.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
