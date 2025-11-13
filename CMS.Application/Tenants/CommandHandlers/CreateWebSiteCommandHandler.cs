using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Commands;
using CMS.Application.Tenants.Events;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;
using CMS.Domain.Tenants.ValueObjects;

namespace CMS.Application.Tenants.CommandHandlers
{
    public class CreateWebSiteCommandHandler : IAppRequestHandler<CreateWebSiteCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWebSiteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateWebSiteCommand request, CancellationToken cancellationToken)
        {
            var webSiteRepository = _unitOfWork.GetRepository<WebSite>();

            var contactInfo = new ContactInfo(request.Email, request.PhoneNumber, request.Address);

            var webSite = new WebSite(contactInfo, request.LogoUrl, request.CompanyName, request.IsActive, request.Description);

            await webSiteRepository.InsertAsync(webSite);

            webSite.AddDomainEvent(new WebSiteCreatedEvent(webSite));

            await _unitOfWork.SaveChangesAsync();

            return webSite.Id;
        }
    }
}
