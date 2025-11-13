using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Queries;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Tenants.QueryHandlers
{
    public class GetDefaultLanguageCodeQueryHandler : IAppRequestHandler<GetDefaultLanguageCodeQuery, string?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDefaultLanguageCodeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string?> Handle(GetDefaultLanguageCodeQuery request, CancellationToken cancellationToken)
        {
            var webSiteRepository = _unitOfWork.GetRepository<WebSite>();

            var website = await webSiteRepository.GetAsync(
                predicate: x => x.Id == request.WebSiteId,
                includeDeleted: false,
                include: query => query.Include(x => x.SupportedLanguages)
                  .ThenInclude(sl => sl.Language));

            if (website == null)
            {
                throw new Exception("Website can not be found...");
            }

            var defaultLanguage = website.GetDefaultLanguage();

            if (defaultLanguage is null)
            {
                return null;
            }

            return defaultLanguage.Language.Code;
        }
    }
}
