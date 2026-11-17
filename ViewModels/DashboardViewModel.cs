namespace ProductValidationMVC.ViewModels;

public sealed class DashboardViewModel
{
    public int ProductCount { get; init; }
    public decimal InventoryValue { get; init; }
    public int CategoryCount { get; init; }
    public int TotalQuantity { get; init; }
    public IReadOnlyList<Models.Product> RecentProducts { get; init; } = [];
    public IReadOnlyList<CategorySummary> Categories { get; init; } = [];
}

public sealed record CategorySummary(string Name, int Count, decimal Value);
