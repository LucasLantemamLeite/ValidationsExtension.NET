using System;

namespace Validation.ExceptonExtension;

/// <summary>
/// Uma classe usada para validar datas que são maior que a data atual.
/// </summary>
public class BirthDateValidationException : Exception
{
    /// <summary>
    /// Uma nova instancia da classe <see cref="BirthDateValidationException"/>
    /// </summary>
    /// <param name="message">A mensagem de erro que será utilizada</param>
    public BirthDateValidationException(string message)
    : base(message) { }

    /// <summary>
    /// Uma função que verifica se a data recebida é menor que a data atual.
    /// Case contrário, lança a Exception <see cref="BirthDateValidationException"/>
    /// </summary>
    /// <param name="birth_date">Um DateTime que será usado para vereficação</param>
    /// <exception cref="BirthDateValidationException">Se a data for maior que a atual ou ser null</exception>
    public static void ValidationThrow(DateTime? birth_date)
    {
        if (birth_date == null) // Verefica se a data não é null
            throw new BirthDateValidationException($"Date Cannot be null."); // Lança a Exception BirthDateValidationException => Date cannot be null.

        if (birth_date >= DateTime.Now) // Verefica se a data é menor que a data atual
            throw new BirthDateValidationException($"{birth_date:dd/MM/yyyy} cannot be greater than the current date."); // Lança a Exception BirthDateValidationException => {birth_date} cannot be greater than the current date.
    }
}