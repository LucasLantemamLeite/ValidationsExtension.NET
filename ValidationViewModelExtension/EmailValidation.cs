using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Validation.ViewModel;

<<<<<<< HEAD
/// <summary>
/// A class used to validate if the email address is in a valid format.
/// </summary>
public class EmailAddresDomain : ValidationAttribute
{
    /// <summary>
    /// Validates if the provided email is in a valid format.
    /// </summary>
=======
public class EmailAddresDomain : ValidationAttribute
{
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
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
<<<<<<< HEAD
}
=======
}
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
