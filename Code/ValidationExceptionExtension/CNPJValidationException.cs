using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Validation.ExceptionExtension
{
    /// <summary>
    /// Exception class for validating CNPJ numbers.
    /// Throws an exception if the CNPJ is invalid.
    /// </summary>
    public class CNPJValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CNPJValidationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public CNPJValidationException(string message) : base(message) { }

        /// <summary>
        /// Validates the provided CNPJ string and throws an exception if it is invalid.
        /// </summary>
        /// <param name="cpnj">The CNPJ string to validate.</param>
        /// <exception cref="CNPJValidationException">Thrown when the CNPJ format or verification digits are invalid.</exception>
        public static void ValidationThrow(string cpnj)
        {
            var cnpjRegex = @"^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}\-?\d{2}$";

            if (string.IsNullOrEmpty(cpnj))
                return;

            // Check the CNPJ format using regex
            if (!Regex.IsMatch(cpnj, cnpjRegex))
                throw new CNPJValidationException($"The CNPJ '{cpnj}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.");

            // Remove formatting characters
            var cnpjFormat = cpnj.ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "");

            // Convert the CNPJ string into a list of integer digits
            List<int> digits = cnpjFormat.Select(c => int.Parse(c.ToString())).ToList();

            // Array of multipliers used to calculate the verification digits
            var multArray = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Take the first 12 digits to calculate the first verification digit
            var originalDigits = digits.Take(12).ToList();

            var sum = 0;

            // Calculate the first verification digit
            for (int i = 0; i < 12; i++)
            {
                sum += originalDigits[i] * multArray[i + 1]; // Shift by +1 to align with the multiplier array
            }

            // Add the first verification digit
            originalDigits.Add((sum % 11 == 0 || sum % 11 == 1) ? 0 : (11 - (sum % 11)));

            sum = 0;

            // Calculate the second verification digit
            for (int i = 0; i < 13; i++)
            {
                sum += originalDigits[i] * multArray[i];
            }

            // Add the second verification digit
            originalDigits.Add((sum % 11 == 0 || sum % 11 == 1) ? 0 : (11 - (sum % 11)));

            // Compare the calculated CNPJ with the provided CNPJ (including verification digits)
            if (string.Join("", originalDigits) != cnpjFormat)
                throw new CNPJValidationException($"Invalid CNPJ: '{cpnj}', the verification digits are incorrect");

            return;
        }
    }
}