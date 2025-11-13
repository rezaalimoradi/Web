namespace Shared.Utilities
{
    public class PagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }

        public string? SearchTerm { get; set; }

        public string? StoreIds { get; set; }
        public string? PeopleIds { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        // اضافه کردن فیلترها
        public string? FilterOperator { get; set; }
        public string? FilterValue { get; set; }


        public string? SaleAmountFilterOperator { get; set; }
        public decimal? SaleAmountFilterValue { get; set; }
        public string? productTitleFilter { get; set; }
    }

}
