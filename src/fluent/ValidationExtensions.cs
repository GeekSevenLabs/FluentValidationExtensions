// ReSharper disable once CheckNamespace
namespace FluentValidation;

public static class ValidationExtensions
{
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <typeparam name="T">Type of object being validated</typeparam>
    extension<T>(IRuleBuilder<T, string?> ruleBuilder)
    {
        /// <summary>
        /// - Defines a 'CNPJ' validator on the current rule builder.
        /// - Validation will fail if the property is the value is an invalid CNPJ
        /// - Case the valeu is a null or empty string, the validation will pass
        /// </summary>
        /// <returns>a rule builder with cnpj validation included</returns>
        public IRuleBuilderOptions<T, string?> IsValidCnpj()
        {
            return ruleBuilder.SetValidator(new CnpjValidator<T>());
        }

        /// <summary>
        /// - Defines a 'CNPJ' validator on the current rule builder.
        /// - CNPJ validation will fail if the property is the value is an invalid pattern CNPJ
        /// - Case the valeu is a null or empty string, the validation will pass
        /// </summary>
        /// <param name="masked">Indicates if the validation should use the masked CNPJ pattern</param>
        public IRuleBuilderOptions<T, string?> IsValidCnpjPattern(bool masked = true)
        {
            return ruleBuilder.SetValidator(new CnpjPatternValidator<T>(masked: masked));
        }

        /// <summary>
        /// - Defines a 'CPF' validator on the current rule builder.
        /// - Validation will fail if the property is the value is an invalid CPF
        /// - Case the valeu is a null or empty string, the validation will pass
        /// </summary>
        /// <returns>a rule builder with CNPJ validation included</returns>
        public IRuleBuilderOptions<T, string?> IsValidCpf()
        {
            return ruleBuilder.SetValidator(new CpfValidator<T>());
        }

        /// <summary>
        /// - Defines a 'CPF' validator on the current rule builder.
        /// - CPF validation will fail if the property is the value is an invalid pattern CPF
        /// - Case the valeu is a null or empty string, the validation will pass
        /// </summary>
        /// <param name="masked">Indicates if the validation should use the masked CPF pattern</param>
        /// <returns>a rule builder with CPF validation included</returns>
        public IRuleBuilderOptions<T, string?> IsValidCpfPattern(bool masked = true)
        {
            return ruleBuilder.SetValidator(new CpfPatternValidator<T>(masked: masked));
        }
    }
}
