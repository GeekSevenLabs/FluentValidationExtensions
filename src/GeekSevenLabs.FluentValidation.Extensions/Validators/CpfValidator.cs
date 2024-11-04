using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

public class CpfValidator<T> :  PropertyValidator<T, string?>, IPropertyValidator<T, string?>
{
    private readonly string? _errorMessage;

    public CpfValidator(string errorMessage) => _errorMessage = errorMessage;
    public CpfValidator() : this("O CPF é inválido!") { }
    
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return true;
        var isValid = CadastroPessoaFisica.IsValid(value);
        return isValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
    }

    public override string Name => "CpfValidator";
}