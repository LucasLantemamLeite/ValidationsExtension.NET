using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Validation.ExceptonExtension
{
    /// <summary>
    /// A custom exception class used for CPF validation errors.
    /// </summary>
    public class CPFValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CPFValidationException"/> class with a specific error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CPFValidationException(string message) : base(message) { }

        /// <summary>
        /// Validates the given CPF. If invalid, it throws a <see cref="CPFValidationException"/>.
        /// </summary>
        /// <param name="cpf">The CPF number to be validated.</param>
        /// <exception cref="CPFValidationException">Thrown if the CPF is invalid or has incorrect verification digits.</exception>
        public static void ValidationThrow(string cpf)
        {
            var cpfRegex = @"^\d{3}(\.)?\d{3}(\.)?\d{3}(\-)?\d{2}$";

            // Check if the value matches the CPF format (with or without punctuation)
            if (!Regex.IsMatch(cpf, cpfRegex))
                throw new CPFValidationException($"The CPF '{cpf}' is in an invalid format or does not have exactly 11 digits. It can optionally include dots (.) and hyphen (-). Please follow the pattern: XXX.XXX.XXX-XX or XXXXXXXXXXX.");


            // Remove formatting characters (dots and dashes)
            var cpfFormat = cpf.Trim().Replace(".", "").Replace("-", "");

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

            // Compare the calculated CPF digits with the original CPF
            if (string.Join("", originalDigits) != cpfFormat)
                throw new CPFValidationException($"Invalid CPF: '{cpf}', the verification digits are incorrect");
        }
    }
}