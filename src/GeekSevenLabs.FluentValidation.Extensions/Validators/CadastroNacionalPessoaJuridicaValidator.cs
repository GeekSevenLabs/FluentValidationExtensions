using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

internal class CadastroNacionalPessoaJuridicaValidator<T, TProperty> :  PropertyValidator<T, TProperty>, IPropertyValidator<T, TProperty>
{
    private readonly string? _errorMessage;

    public CadastroNacionalPessoaJuridicaValidator(string errorMessage) => _errorMessage = errorMessage;
    public CadastroNacionalPessoaJuridicaValidator() : this("O CNPJ é inválido!") { }
    
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var cnpj = value?.ToString() ?? string.Empty;
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);
        return isValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
    }

    public override string Name => "CadastroNacionalPessoaJuridicaValidator";
}