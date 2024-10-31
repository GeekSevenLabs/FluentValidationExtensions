using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;

namespace GeekSevenLabs.FluentValidation.Extensions.Tests.Validators;

public class CadastroNacionalPessoaJuridicaValidatorTests
{
    
    [Theory]
    [InlineData("11.434.325/0001-07")]
    [InlineData("0F.QU3.WLW/6SCR-69")]
    public void WhenCadastroNacionalPessoaJuridicaIsValidThenShouldBeValid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator
                .RuleFor(person => person.Cnpj)
                .IsValidCadastroNacionalPessoaJuridica()
        );
        
        validator
            .TestValidate(new Person { Cnpj = cnpj })
            .ShouldNotHaveValidationErrorFor(person => person.Cnpj);
    }
    
    [Fact]
    public void WhenCadastroNacionalPessoaJuridicaIsInvalidThenSetCustomMessageAndValidatorShouldBeFail()
    {
        const string customMessage = "Custom Message";

        var validator = new TestExtensionsValidator(
            validator => validator
                .RuleFor(person => person.Cnpj)
                .IsValidCadastroNacionalPessoaJuridica()
                .WithMessage(customMessage)
        );
        
        validator
            .TestValidate(new Person { Cnpj = "123.123.123-12" })
            .ShouldHaveValidationErrorFor(person => person.Cnpj)
            .WithErrorMessage(customMessage)
            .WithErrorCode("CadastroNacionalPessoaJuridicaValidator");
    }
    
    [Theory]
    [InlineData("14.442.344/1210-57")] 
    [InlineData("11.434.3215/00123-57")] 
    [InlineData("A11.434.325/0001-07")]
    [InlineData("11.434.325/0001-07a")]
    [InlineData("00.000.000/0000-00")]
    public void WhenCadastroNacionalPessoaJuridicaIsInvalidThenShouldBeInvalid(string cnpj)
    {
        var validator = new TestExtensionsValidator(
            validator => validator
                .RuleFor(person => person.Cnpj)
                .IsValidCadastroNacionalPessoaJuridica()
        );
        
        var result = validator.Validate(new Person { Cnpj = cnpj });

        result.IsValid.Should().BeFalse();
    }
}
