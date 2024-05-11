using System.Text.RegularExpressions;

namespace Restaurant.Domain.Common.Converters;

public static class StringExtension
{
    public static string LeaveUriAvailableLetters(this string text)
    {
        return Regex.Replace(text, @"[^a-zA-Z0-9-]", "");
    }
}