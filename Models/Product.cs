namespace ProductValidationMVC.Models;

public sealed class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public required string Category { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedDate { get; set; }
}
