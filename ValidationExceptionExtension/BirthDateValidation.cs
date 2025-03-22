using System;

namespace Validation.ExceptonExtension;

/// <summary>
/// A class used to validate dates that are greater than the current date.
/// </summary>
public class BirthDateValidationException : Exception
{
    /// <summary>
    /// A new instance of the <see cref="BirthDateValidationException"/> class
    /// </summary>
    /// <param name="message">The error message to be used</param>
    public BirthDateValidationException(string message)
    : base(message) { }

    /// <summary>
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
