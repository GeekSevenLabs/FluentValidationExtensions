using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

public class CnpjPatternValidator<T> : PropertyValidator<T, string?>, IPropertyValidator<T, string?>, IRegularExpressionValidator
{
    private readonly string? _errorMessage;
    private readonly bool _masked;

    public CnpjPatternValidator(string errorMessage = "Formato de CNPJ inválido.", bool masked = true)
    {
        _errorMessage = errorMessage;
        _masked = masked;

        var pattern = masked
            ? CadastroNacionalPessoaJuridica.CreateMaskedCnpjRegex()
            : CadastroNacionalPessoaJuridica.CreateUnmaskedCnpjRegex();

        Expression = $"^$|({pattern})";
    }

    public override string Name => "CnpjPatternValidator";
    public string Expression { get; private set; }

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (value.IsNullOrWhiteSpace()) return true;

        var result = CadastroNacionalPessoaJuridica.IsValidPattern(value);

        return result switch
        {
            { IsValidMasked: true } when _masked => true,
            { IsValidUnmasked: true } when !_masked => true,
            _ => false
        };
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
    }
}