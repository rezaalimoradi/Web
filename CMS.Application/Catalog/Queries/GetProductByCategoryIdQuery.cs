using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

public class GetProductByCategoryIdQuery : IAppRequest<ResultModel<IPagedList<Product>>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public long? CategoryId { get; set; }

    // مرتب‌سازی
    public string? SortBy { get; set; } // "price-low", "price-high", "date", "popularity"

    // فیلترها
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public string? Brand { get; set; }
    public string? PerfumeType { get; set; }
    public string? Size { get; set; }

    public string? SearchTerm { get; set; }
}
