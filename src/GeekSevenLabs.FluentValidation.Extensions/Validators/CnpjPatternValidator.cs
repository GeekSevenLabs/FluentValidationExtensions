using System.Text.RegularExpressions;
using GeekSevenLabs.Utilities.Documents;

namespace FluentValidation.Validators;

public class CnpjPatternValidator<T, TProperty> :  PropertyValidator<T, TProperty>, IPropertyValidator<T, TProperty>
{
    private readonly string? _errorMessage;
    private readonly bool _masked;
    
    public CnpjPatternValidator(string errorMessage = "Formato de CNPJ inválido.", bool masked = true)
    {
        _errorMessage = errorMessage;
        _masked = masked;

        Pattern = masked switch
        {
            true  => CadastroNacionalPessoaJuridica.CreateMaskedCnpjRegex(),
            false => CadastroNacionalPessoaJuridica.CreateUnmaskedCnpjRegex()
        };
    }

    public Regex Pattern { get; private set; }
    
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var cnpj = value?.ToString();
        
        if(cnpj.IsNullOrEmpty()) return true; 
        
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

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

    public override string Name => "CnpjPatternValidator";
}