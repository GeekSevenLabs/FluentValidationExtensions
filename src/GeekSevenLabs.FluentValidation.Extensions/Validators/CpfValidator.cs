using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

internal class CpfValidator<T, TProperty> :  PropertyValidator<T, TProperty>, IPropertyValidator<T, TProperty>
{
    private readonly string? _errorMessage;

    public CpfValidator(string errorMessage) => _errorMessage = errorMessage;
    public CpfValidator() : this("O CPF é inválido!") { }
    
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var cpf = value?.ToString();
        if (string.IsNullOrWhiteSpace(cpf)) return true;
        var isValid = CadastroPessoaFisica.IsValid(cpf);
        return isValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
    }

    public override string Name => "CpfValidator";
}