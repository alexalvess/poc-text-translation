namespace poc_translate.Services.HttpProxy;

public static class Responses
{
    public record TextTranslations
    {
        public List<Translation> Translations { get; init; }
    }

    public record Translation
    {
        public string Text { get; init; }

        public string To { get; init; }
    }
}
