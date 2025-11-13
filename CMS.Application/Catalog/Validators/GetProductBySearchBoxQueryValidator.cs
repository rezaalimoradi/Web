using CMS.Application.Catalog.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductBySearchBoxQueryValidator : AbstractValidator<GetProductBySearchBoxQuery>
    {
        private readonly IRepository<Product> _repository;
        private readonly ITenantContext _tenantContext;

        public GetProductBySearchBoxQueryValidator(IRepository<Product> repository, ITenantContext tenantContext)
        {
            _repository = repository;
            _tenantContext = tenantContext;
        }

        //private async Task<bool> ExistInTenant(long id, CancellationToken cancellationToken)
        //{
        //    return await _repository.Table
        //        .AnyAsync(c => c.CategoryId == id , cancellationToken);
        //}
    }
}
