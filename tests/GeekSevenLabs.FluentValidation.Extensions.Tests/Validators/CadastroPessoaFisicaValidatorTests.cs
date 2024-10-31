using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;

namespace GeekSevenLabs.FluentValidation.Extensions.Tests.Validators;

public class CadastroPessoaFisicaValidatorTests
{
    
    [Fact]
    public void WhenCadastroPessoaFisicaIsValidThenShouldBeValid()
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(r => r.Cpf).IsValidCadastroPessoaFisica()
        );
        
        validator
            .TestValidate(new Person { Cpf = "822.420.106-62" })
            .ShouldNotHaveValidationErrorFor(person => person.Cpf);
    }
    
    [Fact]
    public void WhenCadastroPessoaFisicaIsInvalidThenSetCustomMessageAndValidatorShouldBeFail()
    {
        const string customMessage = "Custom Message";

        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(r => r.Cpf).IsValidCadastroPessoaFisica().WithMessage(customMessage)
        );
        
        validator
            .TestValidate(new Person { Cpf = "123.123.123-12" })
            .ShouldHaveValidationErrorFor(person => person.Cpf)
            .WithErrorMessage(customMessage)
            .WithErrorCode("CadastroPessoaFisicaValidator");
    }
    
    [Theory]
    [InlineData("144.442.344-57")]
    [InlineData("543.434.321-76")]
    [InlineData("A822.420.106-62")]
    [InlineData("822.420.106-62a")]
    [InlineData("822.420.106-6")]
    [InlineData("822.420.106-622")]
    [InlineData("000.000.000-00")]
    [InlineData("111.111.111-11")]
    [InlineData("222.222.222-22")]
    [InlineData("14444234457")]
    [InlineData("54343432176")]
    [InlineData("A82242010662")]
    [InlineData("11111111111")]
    [InlineData("22222222222")]
    public void WhenCadastroPessoaFisicaIsInvalidThenShouldBeInvalid(string cadastroPessoaFisica)
    {
        var validator = new TestExtensionsValidator(
            validator => validator.RuleFor(r => r.Cpf).IsValidCadastroPessoaFisica()
        );
        
        var result = validator.Validate(new Person { Cpf = cadastroPessoaFisica });

        result.IsValid.Should().BeFalse();
    }
}
