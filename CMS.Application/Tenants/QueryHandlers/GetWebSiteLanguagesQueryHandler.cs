using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Dtos;
using CMS.Application.Tenants.Queries;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Tenants.QueryHandlers
{
    public class GetWebSiteLanguagesQueryHandler : IAppRequestHandler<GetWebSiteLanguagesQuery, List<WebSiteLanguageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWebSiteLanguagesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WebSiteLanguageDto>> Handle(GetWebSiteLanguagesQuery request, CancellationToken cancellationToken)
        {
            var websiteRepository = _unitOfWork.GetRepository<WebSite>();

            var website = await websiteRepository.GetAsync(
                predicate: x => x.Id == request.WebSiteId,
                includeDeleted: false,
                include: query => query.Include(x => x.SupportedLanguages)
                                .ThenInclude(x => x.Language));

            if (website == null)
            {
                throw new Exception("Website can not be found...");
            }

            return website.SupportedLanguages.Select(x => new WebSiteLanguageDto()
            {
                Code = x.Language.Code,
                Id = x.Id,
                IsDefault = x.IsDefault
            }).ToList();
        }
    }
}
