using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using FluentValidation.Validators;
using GeekSevenLabs.Utilities.Documents;

namespace GeekSevenLabs.FluentValidation.Extensions.Tests.Validators;

public class CpfPatternValidatorTests
{
    private const string ErrorCode = "CpfPatternValidator";
    private const string DefaultErrorMessage = "Formato de CPF inválido.";
    
    [Fact]
    public void ShouldNotHaveValidationErrorWhenCpfIsNull()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern()
        );

        validator
            .TestValidate(new Person { Cpf = null! })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Fact]
    public void ShouldNotHaveValidationErrorWhenCpfIsEmpty()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern()
        );

        validator
            .TestValidate(new Person { Cpf = string.Empty })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Theory]
    [InlineData("111.222.333-4A")]
    [InlineData("111222.333-4A")]
    [InlineData("111.222333-4A")]
    [InlineData("11.222.333-44")]
    [InlineData("11.222.333-444")]
    public void ShouldHaveValidationErrorWhenCpfMaskedIsInvalid(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern()
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldHaveValidationErrorFor(person => person.Cpf)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage(DefaultErrorMessage);
    }
    
    [Theory]
    [InlineData("1112223334A")]
    [InlineData("11122233344")]
    [InlineData("111222333444")]
    [InlineData("1112223334444")]
    [InlineData("11122233344444")]
    public void ShouldHaveValidationErrorWhenCpfUnmaskedIsInvalid(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern()
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldHaveValidationErrorFor(person => person.Cpf)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage(DefaultErrorMessage);
    }
    
    [Theory]
    [InlineData("111.222.333-44")]
    [InlineData("117.012.764-96")]
    [InlineData("893.102.079-14")]
    public void ShouldNotHaveValidationErrorWhenCpfIsValid(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern()
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Theory]
    [InlineData("11122233344")]
    [InlineData("11701276496")]
    [InlineData("89310207914")]
    public void ShouldNotHaveValidationErrorWhenCpfIsValidUnmasked(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern(masked: false)
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Theory]
    [InlineData("111.222.333-A4")]
    [InlineData("111222.333-4A")]
    public void ShouldHaveValidationErrorWhenCpfMaskedIsInvalidAndErrorMessageIsCustom(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern().WithMessage("CPF inválido.")
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldHaveValidationErrorFor(person => person.Cpf)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage("CPF inválido.");
    }
    
    [Fact]
    public void ShouldHaveContainSpecificPatternWhenCreateInstanceWithMaskedTrue()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern()
        );

        var descriptor = validator.CreateDescriptor();
        var cpfPatternValidator = descriptor.Rules.First().Components.First().Validator as CpfPatternValidator<Person>;

        var pattern = CadastroPessoaFisica.CreateMaskedCpfRegex();
        cpfPatternValidator?.Pattern.ToString().Should().Be($"^$|({pattern})");
    }
    
    [Fact]
    public void ShouldHaveContainSpecificPatternWhenCreateInstanceWithMaskedFalse()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(x => x.Cpf).IsValidCpfPattern(masked: false)
        );

        var descriptor = validator.CreateDescriptor();
        var cpfPatternValidator = descriptor.Rules.First().Components.First().Validator as CpfPatternValidator<Person>;
        
        var pattern = CadastroPessoaFisica.CreateUnmaskedCpfRegex();
        cpfPatternValidator?.Pattern.ToString().Should().Be($"^$|({pattern})");
    }
    
}