using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

public class GetProductBySearchBoxQuery : IAppRequest<ResultModel<IPagedList<Product>>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public long? CategoryId { get; set; }
    public string? SearchTerm { get; set; }
}
