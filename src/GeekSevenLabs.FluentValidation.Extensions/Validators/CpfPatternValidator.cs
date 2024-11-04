using System.Text.RegularExpressions;
using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

public class CpfPatternValidator<T> :  PropertyValidator<T, string?>, IPropertyValidator<T, string?>
{
    private readonly string? _errorMessage;
    private readonly bool _masked;

    public CpfPatternValidator(string errorMessage = "Formato de CPF inválido.", bool masked = true)
    {
        _errorMessage = errorMessage;
        _masked = masked;
        Pattern = _masked ? CadastroPessoaFisica.CreateFormattedCpfRegex() : CadastroPessoaFisica.CreateCpfRegex();
    }
    
    public Regex Pattern { get; private set; }

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if(value.IsNullOrEmpty()) return true; 
        
        var result = CadastroPessoaFisica.IsValidPattern(value);
        
        return (result.IsValidUnmasked && !_masked) || (result.IsValidMasked && _masked);
    }
    
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return string.IsNullOrWhiteSpace(_errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : _errorMessage;
    }

    public override string Name => "CpfPatternValidator";
}