using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Dtos;
using CMS.Application.Tenants.Queries;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Tenants.QueryHandlers
{
    public class GetWebSitesByOwnerIdQueryHandler : IAppRequestHandler<GetWebSitesByOwnerIdQuery, List<WebSiteDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWebSitesByOwnerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WebSiteDto>> Handle(GetWebSitesByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            var websiteRepository = _unitOfWork.GetRepository<WebSite>();

            var websites = await websiteRepository.GetAllAsync(
                predicate: x => x.OwnerId == request.OwnerId,
                func: x=> x.Include(y=> y.Domains).ThenInclude(x=> x.Tld));

            return websites.Select(x => new WebSiteDto()
            {
                Id = x.Id,
                Domain = x.GetDefaultDomain()?.FullDomain ?? "no"
            }).ToList();
        }
    }
}
