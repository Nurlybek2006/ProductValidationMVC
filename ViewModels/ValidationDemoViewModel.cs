using System.ComponentModel.DataAnnotations;

namespace ProductValidationMVC.ViewModels;

public sealed class ValidationDemoViewModel
{
    [Required(ErrorMessage = "Атау міндетті.")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Атау 3–40 таңба болуы керек.")]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "1", "1000000", ErrorMessage = "Баға 1–1 000 000 аралығында болуы керек.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Email міндетті.")]
    [EmailAddress(ErrorMessage = "Email пішімі дұрыс емес.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email растауы міндетті.")]
    [Compare(nameof(Email), ErrorMessage = "Email мекенжайлары сәйкес емес.")]
    public string ConfirmEmail { get; set; } = string.Empty;

    [RegularExpression(@"^[A-Za-zА-Яа-яЁёӘәҒғҚқҢңӨөҰұҮүҺһІі0-9 ._\-]+$", ErrorMessage = "Рұқсат етілмеген таңба бар.")]
    public string? Code { get; set; }
}
