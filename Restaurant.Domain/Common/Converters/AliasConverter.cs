namespace Restaurant.Domain.Common.Converters;

public static class AliasConverter
{
    public static string Convert(string name, ITransliterator transliterator)
    {
        var transliterated = transliterator.Transliterate(name, Alphabet.Latin);
        var uriFriendly = transliterator.ToUriFriendly(transliterated);

        return uriFriendly;
    }
}