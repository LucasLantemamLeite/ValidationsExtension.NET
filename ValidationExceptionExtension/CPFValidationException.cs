using System;
using System.Collections.Generic;
using System.Linq;

namespace Validation.ExceptonExtension;

/// <summary>
/// A class used to validate CPF
/// </summary>
public class CPFValidationException : Exception
{
    /// <summary>
    ///   A new instance of the <see cref="CPFValidationException"/> class
    /// </summary>
    /// <param name="message"></param>
    public CPFValidationException(string message) : base(message) { }

    /// <summary>
    /// A function that checks if the received CPF has 11 characters and performs the verification digit calculation.
    /// Otherwise, it throws the <see cref="CPFValidationException"/>
    /// </summary>
    /// <param name="cpf">The user's CPF</param>
    /// <exception cref="CPFValidationException">If the CPF is not equal to 11 characters or the verification digit is incorrect</exception>
    public static void ValidationThrow(string cpf)
    {
        var cpfFormat = cpf.Replace(".", "").Replace("-", "");

        if (cpfFormat.Length != 11)
            throw new CPFValidationException($"Error in CPF: '{cpf}'. Quantity less than or greater than 11 (not counting punctuation) ");

        List<char> digits = cpfFormat.ToList();
        char[] digitsChar = digits.ToArray();

        var sum = 0;

        for (int i = 10; i >= 2; i--)
        {
            sum += (int)char.GetNumericValue(digitsChar[10 - i]) * i;
        }

        digitsChar[9] = (sum % 11 == 0 || sum % 11 == 1) ? '0' : (char)(11 - (sum % 11) + '0');

        sum = 0;

        for (int i = 11; i >= 2; i--)
        {
            sum += (int)char.GetNumericValue(digitsChar[11 - i]) * i;
        }

        digitsChar[10] = (sum % 11 == 0 || sum % 11 == 1) ? '0' : (char)(11 - (sum % 11) + '0');

        if (string.Join("", digitsChar) != cpfFormat)
            throw new CPFValidationException($"Invalid CPF: '{cpf}', the verification digits are incorrect");
    }
}
