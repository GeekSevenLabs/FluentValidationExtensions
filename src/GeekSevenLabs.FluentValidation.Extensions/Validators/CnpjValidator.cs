using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

public class CnpjValidator<T> :  PropertyValidator<T, string?>, IPropertyValidator<T, string?>
{
    private readonly string? _errorMessage;

    public CnpjValidator(string errorMessage) => _errorMessage = errorMessage;
    public CnpjValidator() : this("O CNPJ é inválido!") { }
    
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return true;
        var isValid = CadastroNacionalPessoaJuridica.IsValid(value);
        return isValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
    }

    public override string Name => "CnpjValidator";
}