using System;
using System.Collections.Generic;
using System.Linq;

namespace Validation.ExceptonExtension;

/// <summary>
<<<<<<< HEAD
/// A class used to validate CPF
=======
/// Uma classe usada para validar CPF
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
/// </summary>
public class CPFValidationException : Exception
{
    /// <summary>
<<<<<<< HEAD
    ///   A new instance of the <see cref="CPFValidationException"/> class
=======
    ///   Uma nova instancia da classe <see cref="CPFValidationException"/>
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
    /// </summary>
    /// <param name="message"></param>
    public CPFValidationException(string message) : base(message) { }

    /// <summary>
<<<<<<< HEAD
    /// A function that checks if the received CPF has 11 characters and performs the verification digit calculation.
    /// Otherwise, it throws the <see cref="CPFValidationException"/>
    /// </summary>
    /// <param name="cpf">The user's CPF</param>
    /// <exception cref="CPFValidationException">If the CPF is not equal to 11 characters or the verification digit is incorrect</exception>
=======
    /// Uma função que verifica se o cpf recebido possui 11 caracteres e faz o calculo do digito vereficador.
    /// Case contrário, lança a Exception <see cref="CPFValidationException"/>
    /// </summary>
    /// <param name="cpf">O CPF do usuário</param>
    /// <exception cref="CPFValidationException">Se o CPF não é igual a 11 ou digito vereficador errado</exception>
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
    public static void ValidationThrow(string cpf)
    {
        var cpfFormat = cpf.Replace(".", "").Replace("-", "");

        if (cpfFormat.Length != 11)
            throw new CPFValidationException($"Error in CPF: '{cpf}'. Quantity less than or greater than 11 (not counting punctuation) ");

        List<char> digits = cpfFormat.ToList();
        char[] digitsChar = digits.ToArray();

        var soma = 0;
        var iDigits = 0;

        for (int i = 10; i >= 2; i--)
        {
            soma += (int)Char.GetNumericValue(digitsChar[iDigits]) * i;
            iDigits++;
        }

<<<<<<< HEAD
        digitsChar[9] = (soma % 11 == 0 || soma % 11 == 1) ? '0' : (char)(11 - (soma % 11) + '0');
=======
        if (soma % 11 == 0 || soma % 11 == 1)
        {
            digitsChar[9] = '0';
        }
        else
        {
            digitsChar[9] = (char)(11 - soma % 11 + '0');
        }
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a

        soma = 0;
        iDigits = 0;

        for (int i = 11; i >= 2; i--)
        {
            soma += (int)Char.GetNumericValue(digitsChar[iDigits]) * i;
            iDigits++;
<<<<<<< HEAD
        }

        digitsChar[10] = (soma % 11 == 0 || soma % 11 == 1) ? '0' : (char)(11 - (soma % 11) + '0');
=======

        }

        if (soma % 11 == 0 || soma % 11 == 1)
        {
            digitsChar[10] = '0';
        }
        else
        {
            digitsChar[10] = (char)(11 - soma % 11 + '0');
        }
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a

        if (string.Join("", digitsChar) != cpfFormat)
            throw new CPFValidationException($"Error, in the CPF: '{cpf}'. The verification digit is incorrect");
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
