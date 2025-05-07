using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Validation.ViewModel
{
    /// <summary>
    /// A custom validation attribute used to validate CPF numbers.
    /// </summary>
    public class CPFValidation : ValidationAttribute
    {
        /// <summary>
        /// Validates the CPF number to ensure it is correct and follows the CPF rules.
        /// </summary>
        /// <param name="value">The CPF value to be validated.</param>
        /// <param name="validationContext">The context of the validation process.</param>
        /// <returns>A ValidationResult that indicates whether the CPF is valid or not.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Check if the value is null or empty
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(ErrorMessage ?? "The CPF cannot be null or empty");

            // Remove formatting characters (dots and dashes)
            var cpfFormat = value.ToString().Trim().Replace(".", "").Replace("-", "");

            // Check if the CPF length is exactly 11 digits
            if (cpfFormat.Length != 11)
                return new ValidationResult(ErrorMessage ?? $"Invalid CPF: '{value}', it must contain exactly 11 digits (excluding punctuation)");

            // Convert the CPF string into a list of digits
            List<char> digits = cpfFormat.ToList();
            char[] digitsChar = digits.ToArray();

            var sum = 0;

            // Calculate the first verification digit
            for (int i = 10; i >= 2; i--)
            {
                sum += (int)char.GetNumericValue(digitsChar[10 - i]) * i;
            }

            // Calculate and assign the first verification digit
            digitsChar[9] = (sum % 11 == 0 || sum % 11 == 1) ? '0' : (char)(11 - (sum % 11) + '0');

            sum = 0;

            // Calculate the second verification digit
            for (int i = 11; i >= 2; i--)
            {
                sum += (int)char.GetNumericValue(digitsChar[11 - i]) * i;
            }

            // Calculate and assign the second verification digit
            digitsChar[10] = (sum % 11 == 0 || sum % 11 == 1) ? '0' : (char)(11 - (sum % 11) + '0');

            // Compare the original CPF with the calculated CPF to verify correctness
            if (string.Join("", digitsChar) != cpfFormat)
                return new ValidationResult(ErrorMessage ?? $"Invalid CPF: '{value}', the verification digits are incorrect");

            // Return success if the CPF is valid
            return ValidationResult.Success;
        }
    }
}
