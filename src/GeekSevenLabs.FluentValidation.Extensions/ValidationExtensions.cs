using FluentValidation.Validators;

namespace FluentValidation;

public static class ValidationExtensions
{
    /// <summary>
    /// - Defines a 'CNPJ' validator on the current rule builder.
    /// - Validation will fail if the property is the value is an invalid CNPJ
    /// - Case the valeu is a null or empty string, the validation will pass 
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <returns>a rule builder with cnpj validation included</returns>
    public static IRuleBuilderOptions<T, string> IsValidCnpj<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CnpjValidator<T, string>());
    }
    
    
    /// <summary>
    /// - Defines a 'CNPJ' validator on the current rule builder.
    /// - CNPJ validation will fail if the property is the value is an invalid pattern CNPJ
    /// - Case the valeu is a null or empty string, the validation will pass
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <param name="masked">Indicates if the validation should use the masked CNPJ pattern</param>
    public static IRuleBuilderOptions<T, string> IsValidCnpjPattern<T>(this IRuleBuilder<T, string> ruleBuilder, bool masked = true)
    {
        return ruleBuilder.SetValidator(new CnpjPatternValidator<T, string>(masked: masked));
    }
    
    /// <summary>
    /// - Defines a 'CPF' validator on the current rule builder.
    /// - Validation will fail if the property is the value is an invalid CPF
    /// - Case the valeu is a null or empty string, the validation will pass
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <returns>a rule builder with CNPJ validation included</returns>
    public static IRuleBuilderOptions<T, string> IsValidCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CpfValidator<T, string>());
    }
    
    /// <summary>
    /// - Defines a 'CPF' validator on the current rule builder.
    /// - CPF validation will fail if the property is the value is an invalid pattern CPF
    /// - Case the valeu is a null or empty string, the validation will pass
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <param name="masked">Indicates if the validation should use the masked CPF pattern</param>
    /// <returns>a rule builder with CPF validation included</returns>
    public static IRuleBuilderOptions<T, string> IsValidCpfPattern<T>(this IRuleBuilder<T, string> ruleBuilder, bool masked = true)
    {
        return ruleBuilder.SetValidator(new CpfPatternValidator<T, string>(masked: masked));
    }
}