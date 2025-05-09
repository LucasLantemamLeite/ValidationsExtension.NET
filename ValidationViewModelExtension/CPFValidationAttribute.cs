using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Validation.ViewModel
{
    /// <summary>
    /// A custom validation attribute used to validate CPF numbers.
    /// </summary>
    public class CPFValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the CPF number to ensure it is correct and follows the CPF rules.
        /// </summary>
        /// <param name="value">The CPF value to be validated.</param>
        /// <param name="validationContext">The context of the validation process.</param>
        /// <returns>A ValidationResult that indicates whether the CPF is valid or not.</returns>
        /// <remarks>
        /// This method first checks if the CPF value is null or empty. If it is not null,
        /// the method uses a regular expression to check if the CPF format is valid.
        /// It also ensures that the CPF has exactly 11 digits after removing any dots or hyphens.
        /// Then, it performs a calculation to validate the CPF using the CPF verification digits.
        /// </remarks>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Check if the value is null or empty
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            var cpfRegex = @"^\d{3}(\.)?\d{3}(\.)?\d{3}(\-)?\d{2}$";

            // Check if the value matches the CPF format (with or without punctuation)
            if (!Regex.IsMatch(value.ToString(), cpfRegex))
                return new ValidationResult(ErrorMessage ?? $"The CPF '{value}' is in an invalid format or does not have exactly 11 digits. It can optionally include dots (.) and hyphen (-). Please follow the pattern: XXX.XXX.XXX-XX or XXXXXXXXXXX.");

            // Remove formatting characters (dots and dashes)
            var cpfFormat = value.ToString().Trim().Replace(".", "").Replace("-", "");

            // Convert the CPF string into a list of digits
            List<int> digits = cpfFormat.Select(c => int.Parse(c.ToString())).ToList();

            // Take the first 9 digits to use in the calculation of the verification digits
            var originalDigits = digits.Take(9).ToList();

            var sum = 0;

            // Calculate the first verification digit
            for (int i = 0; i < 9; i++)
            {
                sum += originalDigits[i] * (10 - i);
            }

            // Add the first verification digit to the list of digits
            originalDigits.Add((sum % 11 == 0 || sum % 11 == 1) ? 0 : (11 - (sum % 11)));

            sum = 0;

            // Calculate the second verification digit
            for (int i = 0; i < 10; i++)
            {
                sum += originalDigits[i] * (11 - i);
            }

            // Add the second verification digit to the list of digits
            originalDigits.Add((sum % 11 == 0 || sum % 11 == 1) ? 0 : (11 - (sum % 11)));

            // Compare the original CPF with the calculated CPF to verify correctness
            if (string.Join("", originalDigits) != cpfFormat)
                return new ValidationResult(ErrorMessage ?? $"Invalid CPF: '{value}', the verification digits are incorrect");

            // Return success if the CPF is valid
            return ValidationResult.Success;
        }
    }
}
