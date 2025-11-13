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
    public class DeleteBlogTagTranslationCommandHandler : IAppRequestHandler<DeleteBlogTagTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteBlogTagTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteBlogTagTranslationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<BlogTag>();

            var blogTag = await repo.GetAsync(
                                                predicate: x => x.Id == request.BlogTagId && x.WebSiteId == _tenantContext.TenantId,
                                                include: x => x.Include(y => y.Translations));
            if (blogTag == null)
            {
                return ResultModel.Fail("BlogTag not found.");
            }

            var translation = blogTag.Translations.FirstOrDefault(x => x.Id == request.Id);
            if (translation == null)
            {
                return ResultModel.Fail("BlogTagTranslation not found.");
            }
            try
            {
                blogTag.RemoveTranslation(translation);

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
