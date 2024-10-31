using FluentValidation.Validators;

namespace FluentValidation;

public static class ValidationExtensions
{
    /// <summary>
    /// Defines a 'CNPJ' validator on the current rule builder.
    /// Validation will fail if the property is null, an empty or the value is an invalid CNPJ         
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <returns>a rule builder with cnpj validation included</returns>
    public static IRuleBuilderOptions<T, string> IsValidCadastroNacionalPessoaJuridica<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CadastroNacionalPessoaJuridicaValidator<T, string>());
    }
    
    /// <summary>
    /// Defines a 'CPF' validator on the current rule builder.
    /// Validation will fail if the property is null, an empty or the value is an invalid CPF         
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <returns>a rule builder with CNPJ validation included</returns>
    public static IRuleBuilderOptions<T, string> IsValidCadastroPessoaFisica<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CadastroPessoaFisicaValidator<T, string>());
    }
}