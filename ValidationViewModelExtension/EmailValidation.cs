using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Validation.ViewModel;

public class EmailAddresDomain : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {

        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return new ValidationResult(ErrorMessage ?? "Email Cannot be null");

        var _email = value.ToString().Trim();

        var regex = @"^[a-zA-Z0-9]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (Regex.IsMatch(_email, regex))
            return ValidationResult.Success;

        return new ValidationResult(ErrorMessage ?? "Invalid E-mail");
    }
}