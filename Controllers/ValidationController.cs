using Microsoft.AspNetCore.Mvc;
using ProductValidationMVC.ViewModels;

namespace ProductValidationMVC.Controllers;

public sealed class ValidationController : Controller
{
    [HttpGet]
    public IActionResult Index() => View(new ValidationDemoViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ValidationDemoViewModel model)
    {
        if (string.Equals(model.Email?.Trim(), "admin@example.com", StringComparison.OrdinalIgnoreCase))
            ModelState.AddModelError(nameof(model.Email), "Бұл email мекенжайын пайдалануға болмайды.");
        ViewData["Submitted"] = true;
        ViewData["Valid"] = ModelState.IsValid;
        return View(model);
    }
}
