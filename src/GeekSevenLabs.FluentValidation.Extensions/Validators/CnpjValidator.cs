using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

public class CnpjValidator<T>(string errorMessage = "O CNPJ é inválido!") :  PropertyValidator<T, string?>, IPropertyValidator<T, string?>, ICnpjValidator
{
    public override string Name => "CnpjValidator";
    
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (value.IsNullOrWhiteSpace()) return true;
        var isValid = CadastroNacionalPessoaJuridica.IsValid(value);
        return isValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return errorMessage.IsNullOrWhiteSpace() ? base.GetDefaultMessageTemplate(errorCode) : errorMessage;
    }
}