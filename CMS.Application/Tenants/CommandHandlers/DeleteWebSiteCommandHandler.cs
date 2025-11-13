using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Commands;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Application.Tenants.CommandHandlers
{
    public class DeleteWebSiteCommandHandler : IAppRequestHandler<DeleteWebSiteCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWebSiteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(DeleteWebSiteCommand request, CancellationToken cancellationToken)
        {
            var webSiteRepository = _unitOfWork.GetRepository<WebSite>();

            var webSite = await webSiteRepository.GetByIdAsync(request.WebSiteId);

            if (webSite is null)
                return false;

            webSiteRepository.Delete(webSite);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
