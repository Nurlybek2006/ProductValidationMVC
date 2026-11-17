using System.ComponentModel.DataAnnotations;
using ProductValidationMVC.Validation;

namespace ProductValidationMVC.ViewModels;

public sealed class ProductCreateViewModel
{
    [Required(ErrorMessage = "Тауар атауын енгізіңіз.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Атау 3–100 таңбадан тұруы керек.")]
    [RegularExpression(@"^[A-Za-zА-Яа-яЁёӘәҒғҚқҢңӨөҰұҮүҺһІі0-9 ._\-]+$", ErrorMessage = "Атауда тек қазақ, орыс, ағылшын әріптері, сандар және бос орын мен сызықша белгілері болуы мүмкін.")]
    [Display(Name = "Тауар атауы")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Сипаттама 500 таңбадан аспауы керек.")]
    [Display(Name = "Сипаттама")]
    public string? Description { get; set; }

    [Range(typeof(decimal), "1", "1000000", ErrorMessage = "Баға 1–1 000 000 аралығында болуы керек.")]
    [Display(Name = "Бағасы")]
    public decimal Price { get; set; }

    [Range(1, 10000, ErrorMessage = "Саны 1–10 000 аралығында болуы керек.")]
    [Display(Name = "Саны")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Категорияны таңдаңыз.")]
    [StringLength(60)]
    [Display(Name = "Категория")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email енгізіңіз.")]
    [EmailAddress(ErrorMessage = "Дұрыс email мекенжайын енгізіңіз.")]
    [Display(Name = "Байланыс email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email мекенжайын растаңыз.")]
    [Compare(nameof(Email), ErrorMessage = "Email мекенжайлары сәйкес келмейді.")]
    [Display(Name = "Email растау")]
    public string ConfirmEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Өндірілген күнді таңдаңыз.")]
    [DataType(DataType.Date)]
    [FutureDateNotAllowed]
    [Display(Name = "Өндірілген күні")]
    public DateTime? ManufactureDate { get; set; }
}
