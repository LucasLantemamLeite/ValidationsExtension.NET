namespace Validation.ViewModel.Test;

public class UserFakeEntity
{
    [CNPJValidation]
    public string? CNPJ { get; set; }
    [CPFValidation]
    public string? CPF { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }

    public UserFakeEntity(string? cnpj = null, string? cpf = null, string? email = null, DateTime? birthDate = null)
    {
        CNPJ = cnpj;
        CPF = cpf;
        Email = email;
        BirthDate = birthDate;
    }
}