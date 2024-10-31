using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

internal class CadastroPessoaFisicaValidator<T, TProperty> :  PropertyValidator<T, TProperty>, IPropertyValidator<T, TProperty>
{
    private readonly string? _errorMessage;

    public CadastroPessoaFisicaValidator(string errorMessage) => _errorMessage = errorMessage;
    public CadastroPessoaFisicaValidator() : this("O CPF é inválido!") { }
    
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var cpf = value?.ToString() ?? string.Empty;
        var isValid = CadastroPessoaFisica.IsValid(cpf);
        return isValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
    }

    public override string Name => "CadastroPessoaFisicaValidator";
}