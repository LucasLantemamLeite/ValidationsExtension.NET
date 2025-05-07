using System;
using System.Text.RegularExpressions;

namespace Validation.ExceptonExtension;

/// <summary>
/// A class used to validate emails containing an '@' and a 'domain', with or without a top-level domain (e.g., ".br")
/// </summary>
public class EmailAddresDomainException : Exception
{

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddresDomainException"/> class    
    /// </summary>
    /// <param name="message">The error message to be used</param>
    public EmailAddresDomainException(string message)
    : base(message) { }

    /// <summary>
    /// Validates if an email is valid.
    /// Otherwise, it throws the <see cref="EmailAddresDomainException"/>
    /// </summary>
    /// <param name="email">The email to be validated</param>
    /// <exception cref="EmailAddresDomainException">If the email is not valid</exception>
    public static void ValidationThrow(string email)
    {
        var regex = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (!Regex.IsMatch(email, regex))
            throw new EmailAddresDomainException($"{email} is not a valid email");
    }
}
