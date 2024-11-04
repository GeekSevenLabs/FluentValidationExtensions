using System.Diagnostics.CodeAnalysis;

namespace FluentValidation;

internal static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(returnValue: false)] this string? value) => string.IsNullOrEmpty(value);
}