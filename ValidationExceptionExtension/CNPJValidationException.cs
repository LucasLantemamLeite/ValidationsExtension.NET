using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Validation.ExceptionExtension;

public class CNPJValidationException : Exception
{

    public CNPJValidationException(string message) : base(message) { }

    public static void ValidationThrow(string cpnj)
    {
        var cnpjRegex = @"^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}\-?\d{2}$";

        if (!Regex.IsMatch(cpnj, cnpjRegex))
            throw new CNPJValidationException($"The CNPJ '{cpnj}' is in an invalid format or does not have exactly 14 digits. It can optionally include dots (.), slash (/) and hyphen (-). Please follow the pattern: XX.XXX.XXX/XXXX-XX or XXXXXXXXXXXXXX.");

        var cnpjFormat = cpnj.ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "");

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

        if (string.Join("", originalDigits) != cnpjFormat)
            throw new CNPJValidationException($"Invalid CNPJ: '{cpnj}', the verification digits are incorrect");

    }
}