using CMS.Application.Catalog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public class UpdateTaxCommand : IAppRequest<ResultModel<TaxDto>>
    {
        public long Id { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
