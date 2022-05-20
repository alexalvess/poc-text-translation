using poc_translate.Services.HttpProxy;

namespace poc_translate.Services;

public class TranslationService : ITranslationService
{
    private readonly ITranslationHttpClient _httpClient;

    public TranslationService(ITranslationHttpClient httpClient)
        => _httpClient = httpClient;

    public async Task<string> TranslateTextAsync(string toTranslate, CancellationToken cancellationToken)
    {
        var response = await _httpClient.TranslateTextAsync(toTranslate, cancellationToken);

        return response.Success ? response.Content.First().Translations.First().Text : toTranslate;
    }
}
