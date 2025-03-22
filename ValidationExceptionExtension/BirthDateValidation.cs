using System;

namespace Validation.ExceptonExtension;

/// <summary>
<<<<<<< HEAD
/// A class used to validate dates that are greater than the current date.
=======
/// Uma classe usada para validar datas que são maior que a data atual.
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
/// </summary>
public class BirthDateValidationException : Exception
{
    /// <summary>
<<<<<<< HEAD
    /// A new instance of the <see cref="BirthDateValidationException"/> class
    /// </summary>
    /// <param name="message">The error message to be used</param>
=======
    /// Uma nova instancia da classe <see cref="BirthDateValidationException"/>
    /// </summary>
    /// <param name="message">A mensagem de erro que será utilizada</param>
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
    public BirthDateValidationException(string message)
    : base(message) { }

    /// <summary>
<<<<<<< HEAD
    /// A function that checks if the received date is smaller than the current date.
    /// Otherwise, it throws the <see cref="BirthDateValidationException"/>
    /// </summary>
    /// <param name="birth_date">A DateTime that will be used for validation</param>
    /// <exception cref="BirthDateValidationException">If the date is greater than the current date or is null</exception>
    public static void ValidationThrow(DateTime? birth_date)
    {
        if (birth_date == null) // Checks if the date is not null
            throw new BirthDateValidationException($"Date cannot be null."); // Throws the BirthDateValidationException => Date cannot be null.

        if (birth_date >= DateTime.Now) // Checks if the date is greater than the current date
            throw new BirthDateValidationException($"{birth_date:dd/MM/yyyy} cannot be greater than the current date."); // Throws the BirthDateValidationException => {birth_date} cannot be greater than the current date.
    }
}
=======
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
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
