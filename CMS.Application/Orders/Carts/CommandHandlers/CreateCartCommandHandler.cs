using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders.Carts;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class CreateCartCommandHandler : IAppRequestHandler<CreateCartCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateCartCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerIdentifier))
                return ResultModel<long>.Fail("CustomerIdentifier is required");

            var cart = new Cart(_tenantContext.TenantId, request.CustomerIdentifier);

            try
            {
                var repository = _unitOfWork.GetRepository<Cart>();
                await repository.InsertAsync(cart);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(cart.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
