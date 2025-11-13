namespace CMS.Application.Catalog.Dtos
{
    public class ProductCategoryTreeDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int Depth { get; set; }
        public bool HasChildren { get; set; }
        public string? ImageUrl { get; set; }

        // برای دراپ‌دان درختی (Vue, Select2, Ant Design, etc.)
        public long Value { get; set; }
        public string Label { get; set; } = string.Empty;
        public List<ProductCategoryTreeDto> Children { get; set; } = new List<ProductCategoryTreeDto>();
    }
}
