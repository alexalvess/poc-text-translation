using poc_translate.Abstractions.Http;

namespace poc_translate.Services.HttpProxy;

public interface ITranslationHttpClient
{
    Task<HttpResponse<List<Responses.TextTranslations>>> TranslateTextAsync(string text, CancellationToken cancellationToken);
}
