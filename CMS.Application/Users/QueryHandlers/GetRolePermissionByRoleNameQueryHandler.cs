using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants;
using CMS.Application.Users.Queries;
using CMS.Domain.Common;
using CMS.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Users.QueryHandlers
{
    public class GetRolePermissionByRoleNameQueryHandler : IAppRequestHandler<GetRolePermissionByRoleNameQuery, List<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetRolePermissionByRoleNameQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<List<Permission>> Handle(GetRolePermissionByRoleNameQuery request, CancellationToken cancellationToken)
        {
            var userGroupPermissionsRepository = _unitOfWork.GetRepository<UserGroupPermission>();

            var temp = await userGroupPermissionsRepository.GetAllAsync(
                      predicate: ugp => ugp.UserId == request.UserId && ugp.GroupPermission.WebSiteRole.Role.NormalizedName == request.RoleName.ToUpper() &&
                                        ugp.GroupPermission.WebSiteRole.WebSiteId == _tenantContext.TenantId,
                      func: x => x.Include(ugp => ugp.GroupPermission)
                                    .ThenInclude(gp => gp.WebSiteRole)
                                  .Include(ugp => ugp.GroupPermission)
                                    .ThenInclude(gp => gp.Permissions));

            return temp.SelectMany(ugp => ugp.GroupPermission.Permissions).Distinct().ToList();
        }
    }
}
