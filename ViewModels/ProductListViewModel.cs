namespace ProductValidationMVC.ViewModels;

public sealed class ProductListViewModel
{
    public IReadOnlyList<Models.Product> Products { get; init; } = [];
    public string? Search { get; init; }
    public string? Category { get; init; }
    public string? Sort { get; init; }
    public IReadOnlyList<string> Categories { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int PageCount => (int)Math.Ceiling(TotalCount / (double)Math.Max(1, PageSize));
}
