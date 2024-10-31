using FluentValidation;

namespace GeekSevenLabs.FluentValidation.Extensions.Tests;

public class TestExtensionsValidator : InlineValidator<Person>
{
    public TestExtensionsValidator(params Action<TestExtensionsValidator>[] actionList)
    {
        foreach (var action in actionList) action.Invoke(this);
    }
}