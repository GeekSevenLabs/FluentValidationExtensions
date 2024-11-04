using System.Text.RegularExpressions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using FluentValidation.Validators;
using GeekSevenLabs.Utilities.Documents;

namespace GeekSevenLabs.FluentValidation.Extensions.Tests.Validators;

public class CnpjPatternValidatorTests
{
    private const string ErroCode = "CnpjPatternValidator";
    private const string DefaultErrorMessage = "Formato de CNPJ inválido.";
    
    [Fact]
    public void ShouldNotHaveValidationErrorWhenCnpjIsNull()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern()
        );

        validator
            .TestValidate(new Person { Cnpj = null! })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Fact]
    public void ShouldNotHaveValidationErrorWhenCnpjIsEmpty()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern()
        );

        validator
            .TestValidate(new Person { Cnpj = string.Empty })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Theory]
    [InlineData("11.222.333/0001-81")]
    [InlineData("11.CCC.333/0001-82")]
    [InlineData("11.222.AAA/0001-83")]
    [InlineData("11.222.AAA/DDDD-83")]
    [InlineData("EE.222.AAA/DDDD-83")]
    public void ShouldNotHaveValidationErrorWhenCnpjMaskedIsValid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern()
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Theory]
    [InlineData("11222333000181")]
    [InlineData("11222AAA000182")]
    [InlineData("11CCC333000183")]
    [InlineData("11CCC333RTR783")]
    public void ShouldNotHaveValidationErrorWhenCnpjUnmaskedIsValid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern(masked: false)
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Theory]
    [InlineData("11.222..333/0001-81")]
    [InlineData("11.222.333/0001-8A")]
    [InlineData("11.CCC.333/0001-822")]
    [InlineData("111.222.AAA/0001-83")]
    [InlineData("11.222.AAA/DDDD-X3")]
    [InlineData("EE.222.AAA/DDDD83")]
    public void ShouldHaveValidationErrorWhenCnpjMaskedIsInvalid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern()
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldHaveValidationErrorFor(person => person.Cnpj)
            .WithErrorCode(ErroCode)
            .WithErrorMessage(DefaultErrorMessage);
    }
    
    [Theory]
    [InlineData("112223330001811")]
    [InlineData("11222333000181A")]
    [InlineData("11222AAA0001822")]
    [InlineData("11CCC3330001833")]
    [InlineData("11CCC333RTR783X")]
    public void ShouldHaveValidationErrorWhenCnpjUnmaskedIsInvalid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern(masked: false)
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldHaveValidationErrorFor(person => person.Cnpj)
            .WithErrorCode(ErroCode)
            .WithErrorMessage(DefaultErrorMessage);
    }
    
    [Theory]
    [InlineData("11.222.333/000181")]
    [InlineData("11.CCC.3330001-82")]
    [InlineData("11.222AAA/0001-83")]
    [InlineData("11222.AAA/DDDD-83")]
    [InlineData("EE222AAA-DDDD83")]
    public void ShouldHaveValidationErrorWhenCnpjMaskedIsInvalidAndErrorMessageIsCustom(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern().WithMessage("CNPJ inválido.")
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldHaveValidationErrorFor(person => person.Cnpj)
            .WithErrorCode(ErroCode)
            .WithErrorMessage("CNPJ inválido.");
    }
    
    [Fact]
    public void ShouldHaveContainSpecificPatternWhenCreateInstanceWithMaskedTrue()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern()
        );

        var descriptor = validator.CreateDescriptor();
        var cnpjPatternValidator = descriptor.Rules.First().Components.First().Validator as CnpjPatternValidator<Person>;

        var pattern = CadastroNacionalPessoaJuridica.CreateMaskedCnpjRegex();
        cnpjPatternValidator?.Pattern.ToString().Should().Be($"^$|({pattern})");
    }
    
    [Fact]
    public void ShouldHaveContainSpecificPatternWhenCreateInstanceWithMaskedFalse()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpjPattern(masked: false)
        );

        var descriptor = validator.CreateDescriptor();
        var cnpjPatternValidator = descriptor.Rules.First().Components.First().Validator as CnpjPatternValidator<Person>;

        var pattern = CadastroNacionalPessoaJuridica.CreateUnmaskedCnpjRegex();
        cnpjPatternValidator?.Pattern.ToString().Should().Be($"^$|({pattern})");
    }
}