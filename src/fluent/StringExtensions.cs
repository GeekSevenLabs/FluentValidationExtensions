using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace FluentValidation;

internal static class StringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(returnValue: false)] this string? value) => string.IsNullOrWhiteSpace(value);
}
