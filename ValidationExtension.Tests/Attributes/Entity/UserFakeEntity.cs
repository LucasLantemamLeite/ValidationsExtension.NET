namespace Validation.ViewModel.Test;

public class UserFakeEntity
{
    [CNPJValidation]
    public string? CNPJ { get; set; }

    [CPFValidation]
    public string? CPF { get; set; }

    [EmailAddressDomain]
    public string? Email { get; set; }

    [ViewModel.MinimumAge(MinimumAge = 18)]
    public DateTime? BirthDate { get; set; }

    [ViewModel.MinimumAge]
    public DateTime? BirthDateDefaultMinimumAge { get; set; }

    [ViewModel.MinimumAge(MinimumAge = -1)]
    public DateTime? BirthDateNegative { get; set; }

    public UserFakeEntity(string? cnpj = null, string? cpf = null, string? email = null, DateTime? birthDate = null)
    {
        CNPJ = cnpj;
        CPF = cpf;
        Email = email;
        BirthDate = birthDate;
    }
}