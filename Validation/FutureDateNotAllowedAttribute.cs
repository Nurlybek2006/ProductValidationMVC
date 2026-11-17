using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProductValidationMVC.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class FutureDateNotAllowedAttribute : ValidationAttribute, IClientModelValidator
{
    public FutureDateNotAllowedAttribute() => ErrorMessage = "Күн бүгіннен кейін болмауы керек.";

    public override bool IsValid(object? value) => value is null || value is not DateTime date || date.Date <= DateTime.Today;

    public void AddValidation(ClientModelValidationContext context)
    {
        Merge(context.Attributes, "data-val", "true");
        Merge(context.Attributes, "data-val-notfuture", ErrorMessage ?? "Күн бүгіннен кейін болмауы керек.");
    }

    private static void Merge(IDictionary<string, string> attributes, string key, string value)
    {
        if (!attributes.ContainsKey(key)) attributes.Add(key, value);
    }
}
