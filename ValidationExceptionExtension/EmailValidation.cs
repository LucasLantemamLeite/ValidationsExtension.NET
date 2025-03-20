using System;
using System.Text.RegularExpressions;

namespace Validation.ExceptonExtension;


/// <summary>
/// Uma classe usada para validar e-mails contendo um '@' um 'dominio' possuindo ou não um orgão (ex: ".br")
/// </summary>
public class EmailAddresDomainException : Exception
{

    /// <summary>
    /// Inicializa uma nova instancia da classe <see cref="EmailAddresDomainException"/>    
    /// </summary>
    /// <param name="message">A mensagem de erro que será utilizada</param>
    public EmailAddresDomainException(string message)
    : base(message) { }

    /// <summary>
    /// Válida se um e-mail é válido.
    /// Caso contrário, lança a Excepton <see cref="EmailAddresDomainException"/>
    /// </summary>
    /// <param name="email">O e-mail que será validado</param>
    /// <exception cref="EmailAddresDomainException">Se o email não é válido</exception>
    public static void ValidationThrow(string email)
    {
        var regex = @"^[a-zA-Z0-9]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (!Regex.IsMatch(email, regex))
            throw new EmailAddresDomainException($"{email} not is a e-mail valid");

    }
}