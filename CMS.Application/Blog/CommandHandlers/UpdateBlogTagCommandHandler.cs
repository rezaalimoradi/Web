using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.CommandHandlers
{
    public class UpdateBlogTagCommandHandler : IAppRequestHandler<UpdateBlogTagCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateBlogTagCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdateBlogTagCommand request, CancellationToken cancellationToken)
        {
            var blogTag = await _unitOfWork.GetRepository<BlogTag>().GetAsync(
                                                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                                                include: x => x.Include(y => y.Translations));
            if (blogTag == null)
            {
                return ResultModel.Fail("BlogTag not found.");
            }

            try
            {
                blogTag.UpdateTranslation(request.WebSiteLanguageId, request.Name, request.Slug);

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
