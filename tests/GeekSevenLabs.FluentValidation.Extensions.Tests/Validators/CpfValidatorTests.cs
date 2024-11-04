using FluentValidation;
using FluentValidation.TestHelper;

namespace GeekSevenLabs.FluentValidation.Extensions.Tests.Validators;

public class CpfValidatorTests
{
    private const string ErrorCode = "CpfValidator";
    private const string DefaultErrorMessage = "O CPF é inválido!";
    
    [Fact]
    public void ShouldNotHaveValidationErrorWhenCpfIsNull()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(person => person.Cpf).IsValidCpf()
        );

        validator
            .TestValidate(new Person { Cpf = null! })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Fact]
    public void ShouldNotHaveValidationErrorWhenCpfIsEmpty()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(person => person.Cpf).IsValidCpf()
        );

        validator
            .TestValidate(new Person { Cpf = string.Empty })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Theory]
    [InlineData("111.222.333-4A")]
    [InlineData("545.222.787-99")]
    [InlineData("111.345.333-42")]
    public void ShouldHaveValidationErrorWhenCpfMaskedIsInvalid(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(person => person.Cpf).IsValidCpf()
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldHaveValidationErrorFor(person => person.Cpf)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage(DefaultErrorMessage);
    }
    
    [Theory]
    [InlineData("1112223334A")]
    [InlineData("54522278799")]
    [InlineData("11134533342")]
    public void ShouldHaveValidationErrorWhenCpfUnmaskedIsInvalid(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(person => person.Cpf).IsValidCpf()
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldHaveValidationErrorFor(person => person.Cpf)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage(DefaultErrorMessage);
    }
    
    [Theory]
    [InlineData("54541528894")]
    [InlineData("38471284502")]
    [InlineData("96918710095")]
    [InlineData("63834446033")]
    [InlineData("84427757227")]
    public void ShouldNotHaveValidationErrorWhenCpfUnmaskedIsValid(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(person => person.Cpf).IsValidCpf()
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Theory]
    [InlineData("545.415.288-94")]
    [InlineData("384.712.845-02")]
    [InlineData("969.187.100-95")]
    [InlineData("638.344.460-33")]
    [InlineData("844.277.572-27")]
    public void ShouldNotHaveValidationErrorWhenCpfMaskedIsValid(string cpf)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(person => person.Cpf).IsValidCpf()
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Theory]
    [InlineData("1112223334A")]
    [InlineData("54522278799")]
    [InlineData("11134533342")]
    public void ShouldHaveValidationErrorWithCustomMessageWhenCpfIsInvalid(string cpf)
    {
        const string customMessage = "Custom message";
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(person => person.Cpf).IsValidCpf().WithMessage(customMessage)
        );

        validator
            .TestValidate(new Person { Cpf = cpf })
            .ShouldHaveValidationErrorFor(person => person.Cpf)
            .WithErrorCode(ErrorCode)
            .WithErrorMessage(customMessage);
    }
    
}