using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductValidationMVC.Data;
using ProductValidationMVC.Models;
using ProductValidationMVC.ViewModels;

namespace ProductValidationMVC.Controllers;

public sealed class ProductsController(ApplicationDbContext db) : Controller
{
    private static readonly string[] CategoryOptions = ["Электроника", "Жабдықтар", "Кеңсе тауарлары", "Аксессуарлар", "Басқа"];

    public async Task<IActionResult> Index(string? search, string? category, string? sort = "newest", int page = 1)
    {
        const int pageSize = 8;
        page = Math.Max(1, page);
        var query = db.Products.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(p => p.Name.Contains(search.Trim()));
        if (!string.IsNullOrWhiteSpace(category)) query = query.Where(p => p.Category == category);
        query = sort switch
        {
            "price-asc" => query.OrderBy(p => p.Price),
            "price-desc" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderByDescending(p => p.CreatedDate)
        };
        var total = await query.CountAsync();
        var model = new ProductListViewModel
        {
            Products = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            Search = search, Category = category, Sort = sort,
            Categories = await db.Products.AsNoTracking().Select(p => p.Category).Distinct().OrderBy(x => x).ToListAsync(),
            Page = page, PageSize = pageSize, TotalCount = total
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create() => View(new ProductCreateViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        if (string.Equals(model.Email?.Trim(), "admin@example.com", StringComparison.OrdinalIgnoreCase))
            ModelState.AddModelError(nameof(model.Email), "Бұл email мекенжайын пайдалануға болмайды.");

        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = CategoryOptions;
            return View(model);
        }

        var product = new Product
        {
            Name = model.Name?.Trim() ?? string.Empty,
            Description = string.IsNullOrWhiteSpace(model.Description) ? null : model.Description.Trim(),
            Price = model.Price,
            Quantity = model.Quantity,
            Category = model.Category,
            Email = model.Email?.Trim() ?? string.Empty,
            CreatedDate = DateTime.UtcNow
        };
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Success), new { id = product.Id });
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return product is null ? NotFound() : View(product);
    }

    public async Task<IActionResult> Success(int id)
    {
        var product = await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return product is null ? NotFound() : View(product);
    }
}
