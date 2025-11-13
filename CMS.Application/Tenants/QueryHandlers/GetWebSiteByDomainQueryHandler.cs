using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Dtos;
using CMS.Application.Tenants.Queries;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Tenants.QueryHandlers
{
    public class GetWebSiteByDomainQueryHandler : IAppRequestHandler<GetWebSiteByDomainQuery, WebSiteDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWebSiteByDomainQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WebSiteDto?> Handle(GetWebSiteByDomainQuery request, CancellationToken cancellationToken)
        {
            var websiteDomainRepository = _unitOfWork.GetRepository<WebSiteDomain>();

            var domain = await websiteDomainRepository.GetAsync(
                                                            predicate: x => x.DomainName == request.Domain,
                                                            includeDeleted: false,
                                                              include: x => x.Include(y => y.WebSite)
                                                                                .ThenInclude(y => y.Themes)
                                                                                    .ThenInclude(y=>y.Theme));

            if (domain is null)
            {
                throw new Exception("Domain not found");
            }

            return new WebSiteDto()
            {
                Domain = domain.DomainName,
                Id = domain.WebSiteId,
                Theme = domain.WebSite.GetDefaultTheme()?.Theme?.Name ?? "Default" 
            };
        }
    }
}
