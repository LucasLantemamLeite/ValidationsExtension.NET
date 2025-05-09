using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Validation.ViewModel
{
    /// <summary>
    /// A custom validation attribute used to validate CNPJ numbers.
    /// </summary>
    public class CNPJValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the CNPJ number to ensure it is correct and follows the CNPJ rules.
        /// </summary>
        /// <param name="value">The CNPJ value to be validated.</param>
        /// <param name="validationContext">The context of the validation process.</param>
        /// <returns>A ValidationResult that indicates whether the CNPJ is valid or not.</returns>
        /// <remarks>
        /// This method first checks if the CNPJ value is null or empty. If it is not null,
        /// the method uses a regular expression to check if the CNPJ format is valid.
        /// It also ensures that the CNPJ has exactly 14 digits after removing any dots, slashes, or hyphens.
        /// Then, it performs a calculation to validate the CNPJ using the CNPJ verification digits.
        /// </remarks>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Check if the value is null or empty
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            var cnpjRegex = @"^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}\-?\d{2}$";

            // Check if the value matches the CNPJ format (with or without punctuation)
            if (!Regex.IsMatch(value.ToString(), cnpjRegex))
                return new ValidationResult(ErrorMessage ?? $"The CNPJ '{value}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.");

            // Remove formatting characters (dots, slashes, and hyphens)
            var cnpjFormat = value.ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "");

            // Convert the CNPJ string into a list of digits
            List<int> digits = cnpjFormat.Select(c => int.Parse(c.ToString())).ToList();

            // Take the first 12 digits to use in the calculation of the verification digits
            var originalDigits = digits.Take(12).ToList();

            var sum = 0;

            // Calculate the first verification digit
            for (int i = 0; i < 12; i++)
            {
                sum += originalDigits[i] * (i < 4 ? 6 - i : 5 - (i - 4));
            }

            // Add the first verification digit to the list
            originalDigits.Add((sum % 11 == 0 || sum % 11 == 1) ? 0 : (11 - (sum % 11)));

            sum = 0;

            // Calculate the second verification digit
            for (int i = 0; i < 13; i++)
            {
                sum += originalDigits[i] * (i < 4 ? 6 - i : 5 - (i - 4));
            }

            // Add the second verification digit to the list
            originalDigits.Add((sum % 11 == 0 || sum % 11 == 1) ? 0 : (11 - (sum % 11)));

            // Compare the original CNPJ with the calculated CNPJ to verify correctness
            if (string.Join("", originalDigits) != cnpjFormat)
                return new ValidationResult(ErrorMessage ?? $"Invalid CNPJ: '{value}', the verification digits are incorrect");

            // Return success if the CNPJ is valid
            return ValidationResult.Success;
        }
    }
}