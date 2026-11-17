using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductValidationMVC.Data;
using ProductValidationMVC.ViewModels;

namespace ProductValidationMVC.Controllers;

public sealed class HomeController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = db.Products.AsNoTracking();
        var model = new DashboardViewModel
        {
            ProductCount = await products.CountAsync(),
            InventoryValue = await products.SumAsync(p => (decimal?)(p.Price * p.Quantity)) ?? 0,
            CategoryCount = await products.Select(p => p.Category).Distinct().CountAsync(),
            TotalQuantity = await products.SumAsync(p => (int?)p.Quantity) ?? 0,
            RecentProducts = await products.OrderByDescending(p => p.CreatedDate).Take(5).ToListAsync(),
            Categories = (await products.Select(p => new { p.Category, p.Price, p.Quantity }).ToListAsync())
                .GroupBy(p => p.Category)
                .Select(g => new CategorySummary(g.Key, g.Count(), g.Sum(p => p.Price * p.Quantity)))
                .OrderByDescending(c => c.Value).Take(5).ToList()
        };
        return View(model);
    }

    public IActionResult Error() => View("Error", new ProductValidationMVC.Models.ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
}
