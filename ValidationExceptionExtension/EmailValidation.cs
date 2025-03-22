using System;
using System.Text.RegularExpressions;

namespace Validation.ExceptonExtension;

<<<<<<< HEAD
/// <summary>
/// A class used to validate emails containing an '@' and a 'domain', with or without a top-level domain (e.g., ".br")
=======

/// <summary>
/// Uma classe usada para validar e-mails contendo um '@' um 'dominio' possuindo ou não um orgão (ex: ".br")
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
/// </summary>
public class EmailAddresDomainException : Exception
{

    /// <summary>
<<<<<<< HEAD
    /// Initializes a new instance of the <see cref="EmailAddresDomainException"/> class    
    /// </summary>
    /// <param name="message">The error message to be used</param>
=======
    /// Inicializa uma nova instancia da classe <see cref="EmailAddresDomainException"/>    
    /// </summary>
    /// <param name="message">A mensagem de erro que será utilizada</param>
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
    public EmailAddresDomainException(string message)
    : base(message) { }

    /// <summary>
<<<<<<< HEAD
    /// Validates if an email is valid.
    /// Otherwise, it throws the <see cref="EmailAddresDomainException"/>
    /// </summary>
    /// <param name="email">The email to be validated</param>
    /// <exception cref="EmailAddresDomainException">If the email is not valid</exception>
=======
    /// Válida se um e-mail é válido.
    /// Caso contrário, lança a Excepton <see cref="EmailAddresDomainException"/>
    /// </summary>
    /// <param name="email">O e-mail que será validado</param>
    /// <exception cref="EmailAddresDomainException">Se o email não é válido</exception>
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
    public static void ValidationThrow(string email)
    {
        var regex = @"^[a-zA-Z0-9]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (!Regex.IsMatch(email, regex))
<<<<<<< HEAD
            throw new EmailAddresDomainException($"{email} is not a valid email");
    }
}
=======
            throw new EmailAddresDomainException($"{email} not is a e-mail valid");

    }
}
>>>>>>> ed8f7428c6b7d5a9ff782ae8385746f1b617694a
