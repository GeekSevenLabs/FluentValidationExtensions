using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

public class CpfValidator<T>(string errorMessage = "O CPF é inválido!") : PropertyValidator<T, string?>, IPropertyValidator<T, string?>, ICpfValidator
{
    public override string Name => "CpfValidator";
    
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (value.IsNullOrWhiteSpace()) return true;
        var isValid = CadastroPessoaFisica.IsValid(value);
        return isValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return errorMessage.IsNullOrWhiteSpace() ? base.GetDefaultMessageTemplate(errorCode) : errorMessage;
    }
}