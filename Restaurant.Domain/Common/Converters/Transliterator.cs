using System.Collections.Immutable;
using System.Text;

namespace Restaurant.Domain.Common.Converters;

public class Transliterator : ITransliterator
{
    public string Transliterate(string text, Alphabet alphabet)
    {
        var transliterated = alphabet switch
        {
            Alphabet.Cyrillic => LatinToCyrillic(text),
            Alphabet.Latin => CyrillicToLatin(text),
            _ => string.Empty
        };

        return transliterated;
    }

    public string ToUriFriendly(string input)
    {
        return input
            .Replace(" ", "-")
            .LeaveUriAvailableLetters()
            .ToLower();
    }

    private static readonly ImmutableDictionary<char, string> CyrillicToLatinMapping = new Dictionary<char, string>
    {
        { 'а', "a"}, { 'А', "A"},
        { 'б', "b"}, { 'Б', "B"},
        { 'в', "v"}, { 'В', "V"},
        { 'г', "h"}, { 'Г', "H"},
        { 'ґ', "g"}, { 'Ґ', "G"},
        { 'д', "d"}, { 'Д', "D"},
        { 'е', "e"}, { 'Е', "E"},
        { 'є', "ye"}, { 'Є', "Ye"},
        { 'ж', "zh"}, { 'Ж', "Zh"},
        { 'з', "z"}, { 'З', "Z"},
        { 'и', "y"}, { 'И', "Y"},
        { 'і', "i"}, { 'І', "I"},
        { 'ї', "yi"}, { 'Ї', "Yi"},
        { 'й', "y"}, { 'Й', "Y"},
        { 'к', "k"}, { 'К', "K"},
        { 'л', "l"}, { 'Л', "L"},
        { 'м', "m"}, { 'М', "M"},
        { 'н', "n"}, { 'Н', "N"},
        { 'о', "o"}, { 'О', "O"},
        { 'п', "p"}, { 'П', "P"},
        { 'р', "r"}, { 'Р', "R"},
        { 'с', "s"}, { 'С', "S"},
        { 'т', "t"}, { 'Т', "T"},
        { 'у', "u"}, { 'У', "U"},
        { 'ф', "f"}, { 'Ф', "F"},
        { 'х', "kh"}, { 'Х', "Kh"},
        { 'ц', "ts"}, { 'Ц', "Ts"},
        { 'ч', "ch"}, { 'Ч', "Ch"},
        { 'ш', "sh"}, { 'Ш', "Sh"},
        { 'щ', "shch"}, { 'Щ', "Shch"},
        { 'ь', "'"}, { 'Ь', "'"},
        { 'ю', "yu"}, { 'Ю', "Yu"},
        { 'я', "ya"}, { 'Я', "Ya"},
        { ' ', " " },
        { '0', "0" },
        { '1', "1" },
        { '2', "2" },
        { '3', "3" },
        { '4', "4" },
        { '5', "5" },
        { '6', "6" },
        { '7', "7" },
        { '8', "8" },
        { '9', "9" }
    }.ToImmutableDictionary();

    private static readonly ImmutableArray<(string Latin, char Cyrillic)> LatinToCyrillicMapping =
        CyrillicToLatinMapping
            .OrderByDescending(v => v.Value.Length)
            .Select(d => (Latin: d.Value, Cyrillic: d.Key))
            .ToImmutableArray();

    private static string CyrillicToLatin(string text)
        => string.Join("", text.ToCharArray().Select(c => CyrillicToLatinMapping[c]));

    private static string LatinToCyrillic(string text)
    {
        StringBuilder accumulator = new();
        var textSpan = text.AsSpan();
        var startIdx = 0;
        var prevStartIdx = startIdx;

        foreach (var (latin, cyrillic) in LatinToCyrillicMapping)
        {
            if (!textSpan[startIdx..].StartsWith(latin))
            {
                continue;
            }

            accumulator.Append(cyrillic);
            startIdx += latin.Length;
            break;
        }

        if (prevStartIdx == startIdx)
            throw new NotSupportedException("...");

        return accumulator.ToString();
    }
}