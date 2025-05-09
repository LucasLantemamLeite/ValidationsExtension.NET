using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Validation.ViewModel;

/// <summary>
/// A custom validation attribute used to check if the email address is in a valid format.
/// </summary>
public class EmailAddressDomain : ValidationAttribute
{
    /// <summary>
    /// Validates if the provided email is in a valid format using a regular expression.
    /// </summary>
    /// <param name="value">The value to validate, expected to be a string representing the email address.</param>
    /// <param name="validationContext">Provides context information about the validation process.</param>
    /// <returns>
    /// Returns a <see cref="ValidationResult"/> indicating whether the email is valid or not.
    /// If invalid, a message will be returned.
    /// </returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Check if the email is null or empty
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;

        var _email = value.ToString().Trim();

        // Regular expression for validating email format
        var regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$";

        // Check if the email matches the regular expression pattern
        if (Regex.IsMatch(_email, regex))
            return ValidationResult.Success;

        // Return error message if the email format is invalid
        return new ValidationResult(ErrorMessage ?? $"'{value}' is an invalid email format");
    }
}

