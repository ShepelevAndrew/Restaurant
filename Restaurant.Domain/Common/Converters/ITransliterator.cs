namespace Restaurant.Domain.Common.Converters;

public interface ITransliterator
{
    string Transliterate(string text, Alphabet alphabet);

    string ToUriFriendly(string input);
}