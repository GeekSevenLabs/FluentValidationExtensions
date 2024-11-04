using FluentValidation;
using FluentValidation.TestHelper;

namespace GeekSevenLabs.FluentValidation.Extensions.Tests.Validators;

public class CnpjValidatorTests
{
    private const string ErrorCode = "CnpjValidator";
    private const string DefaultErrorMessage = "O CNPJ é inválido!";
    
    [Fact]
    public void ShouldNotHaveValidationErrorWhenCnpjIsNull()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpj()
        );

        validator
            .TestValidate(new Person { Cnpj = null! })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Fact]
    public void ShouldNotHaveValidationErrorWhenCnpjIsEmpty()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpj()
        );

        validator
            .TestValidate(new Person { Cnpj = string.Empty })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Theory]
    [InlineData("11.222.333/0001-82")]
    [InlineData("11.CCC.333/0001-82")]
    [InlineData("11.222.AAA/0001-83")]
    [InlineData("11.222.AAA/DDDD-83")]
    [InlineData("EE.222AAA/DDDD-83")]
    [InlineData("Y8.8XP.T41/TOF9-3A")]
    public void ShouldHaveValidationErrorWhenCnpjMaskedIsInvalid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpj()
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldHaveValidationErrorFor(person => person.Cnpj)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage(DefaultErrorMessage);
    }
    
    [Theory]
    [InlineData("11222333000182")]
    [InlineData("11222AAA000182")]
    [InlineData("11CCC333000183")]
    [InlineData("11CCC333RTR783")]
    [InlineData("11111111111111")]
    [InlineData("55555555555555")]
    [InlineData("12312312")]
    [InlineData("12312312312312312")]
    public void ShouldHaveValidationErrorWhenCnpjUnmaskedIsInvalid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpj()
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldHaveValidationErrorFor(person => person.Cnpj)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage(DefaultErrorMessage);
    }
    
    [Theory]
    [InlineData("77.388.873/7420-47")]
    [InlineData("Z0.ZO6.F9W/0UBA-50")]
    [InlineData("43.369.536/3816-40")]
    [InlineData("Y8.8XP.T41/TOF9-37")]
    public void ShouldNotHaveValidationErrorWhenCnpjMaskedIsValid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpj()
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Theory]
    [InlineData("77388873742047")]
    [InlineData("Z0ZO6F9W0UBA50")]
    [InlineData("43369536381640")]
    [InlineData("Y88XPT41TOF937")]
    public void ShouldNotHaveValidationErrorWhenCnpjUnmaskedIsValid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpj()
        );

        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Fact]
    public void ShouldHaveValidationErrorWithCustomMessageWhenCnpjIsInvalid()
    {
        const string customMessage = "CNPJ inválido!";
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cnpj).IsValidCnpj().WithMessage(customMessage)
        );

        validator
            .TestValidate(new Person { Cnpj = "11.222.333/0001-82" })
            .ShouldHaveValidationErrorFor(person => person.Cnpj)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage(customMessage);
    }
}