using CMS.Application.Catalog.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductByCategoryIdQueryValidator : AbstractValidator<GetProductByCategoryIdQuery>
    {
        private readonly IRepository<Product> _repository;
        private readonly ITenantContext _tenantContext;

        public GetProductByCategoryIdQueryValidator(IRepository<Product> repository, ITenantContext tenantContext)
        {
            _repository = repository;
            _tenantContext = tenantContext;
        }

    }
}
