using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Blog.CommandHandlers
{
    public class DeleteBlogTagCommandHandler : IAppRequestHandler<DeleteBlogTagCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteBlogTagCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteBlogTagCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<BlogTag>();

            var blogTag = await repo.GetAsync(
                                                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId);
            if (blogTag == null)
            {
                return ResultModel.Fail("BlogTag not found.");
            }

            try
            {
                repo.Delete(blogTag);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel.Success();
            }
            catch (DomainException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
