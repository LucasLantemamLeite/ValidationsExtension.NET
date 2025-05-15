using System;
using System.Text.RegularExpressions;

namespace Validation.ExceptionExtension;

/// <summary>
/// A class used to validate emails containing an '@' and a 'domain', with or without a top-level domain (e.g., ".br")
/// </summary>
public class EmailAddressDomainException : Exception
{

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddressDomainException"/> class    
    /// </summary>
    /// <param name="message">The error message to be used</param>
    public EmailAddressDomainException(string message)
    : base(message) { }

    /// <summary>
    /// Validates if an email is valid.
    /// Otherwise, it throws the <see cref="EmailAddressDomainException"/>
    /// </summary>
    /// <param name="email">The email to be validated</param>
    /// <exception cref="EmailAddressDomainException">If the email is not valid</exception>
    public static void ValidationThrow(string email)
    {
        var regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$";

        if (string.IsNullOrEmpty(email))
            return;

        if (!Regex.IsMatch(email, regex))
            throw new EmailAddressDomainException($"'{email} is not a valid email");

        return;
    }
}
