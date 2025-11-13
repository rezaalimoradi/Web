using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.Catalog.Commands
{
    public class CreateTaxCommand : IAppRequest<ResultModel<long>>
    {
        [Range(0, 1)]
        public decimal Rate { get; set; }

        public bool IsActive { get; set; } = true;

        public long WebSiteId { get; set; }

        [Required]
        public long WebSiteLanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
