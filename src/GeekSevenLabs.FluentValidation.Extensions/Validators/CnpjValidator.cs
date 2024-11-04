using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

public class CnpjValidator<T, TProperty> :  PropertyValidator<T, TProperty>, IPropertyValidator<T, TProperty>
{
    private readonly string? _errorMessage;

    public CnpjValidator(string errorMessage) => _errorMessage = errorMessage;
    public CnpjValidator() : this("O CNPJ é inválido!") { }
    
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var cnpj = value?.ToString() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(cnpj)) return true;
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);
        return isValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
    }

    public override string Name => "CnpjValidator";
}