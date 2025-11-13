using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Commands;
using CMS.Application.Tenants.Events;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Application.Tenants.CommandHandlers
{
    public class CreateLanguageCommandHandler : IAppRequestHandler<CreateLanguageCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLanguageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var webSiteRepository = _unitOfWork.GetRepository<WebSite>();

            var website = await webSiteRepository.GetAsync(
                predicate: x => x.Id == request.WebsiteId,
                includeDeleted: false,
                x => x.Domains,
                x => x.SupportedLanguages);

            if (website == null)
                throw new KeyNotFoundException("Website not found.");

            var language = new WebSiteLanguage(website.Id, request.LanguageId, request.IsDefault);

            website.AddLanguage(language);

            webSiteRepository.Update(website);

            website.AddDomainEvent(new WebSiteLanguageCreatedEvent(language));

            await _unitOfWork.SaveChangesAsync();

            return language.Id;
        }
    }
}
