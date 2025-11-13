using System.Security.Claims;
using CMS.Application.Tenants;
using CMS.Application.Tenants.Queries;
using CMS.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace CMS.Infrastructure.Common
{
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private string? _currentLanguage;
        private readonly ITenantContext _tenantContext;

        public WorkContext(IHttpContextAccessor httpContextAccessor, IMediator mediator, IConfiguration configuration, ITenantContext tenantContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
            _configuration = configuration;
            _tenantContext = tenantContext;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public long? CurrentUserId
        {
            get
            {
                var idClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return long.TryParse(idClaim, out var id) ? id : null;
            }
        }

        public async Task<string?> CurrentLanguage()
        {
            if (_currentLanguage != null)
                return _currentLanguage;

            var routeData = _httpContextAccessor.HttpContext?.GetRouteData();
            var culture = routeData?.Values["culture"]?.ToString();

            if (!string.IsNullOrWhiteSpace(culture))
            {
                _currentLanguage = culture;
                return _currentLanguage;
            }

            var tenantDefaultLanguage = await _mediator.Send(new GetDefaultLanguageCodeQuery() { WebSiteId = _tenantContext.TenantId });
            if (!string.IsNullOrWhiteSpace(tenantDefaultLanguage))
            {
                _currentLanguage = tenantDefaultLanguage;
                return _currentLanguage;
            }
            _currentLanguage = _configuration["Localization:DefaultCulture"] ?? "fa";

            return _currentLanguage;
        }

        public string? CurrentCurrency => throw new NotImplementedException();

        public decimal? CurrentTaxRate => throw new NotImplementedException();
    }
}
