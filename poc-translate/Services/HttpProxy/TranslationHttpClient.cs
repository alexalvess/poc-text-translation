using Microsoft.Extensions.Options;
using poc_translate.Abstractions.Http;

namespace poc_translate.Services.HttpProxy;

public class TranslationHttpClient : ApplicationHttpClient, ITranslationHttpClient
{
    private readonly TextTranslationHttpClientOptions _options;

    public TranslationHttpClient(IOptions<TextTranslationHttpClientOptions> options, HttpClient httpClient)
        : base(httpClient)
        => (_options) = options.Value;

    public Task<HttpResponse<List<Responses.TextTranslations>>> TranslateTextAsync(string text, CancellationToken cancellationToken)
    {
        var request = new object[] { new Requests.TextToTranslate(text) };
        
        var queryString = new Dictionary<string, string>();
        queryString.Add("api-version", _options.ApiVersion);
        queryString.Add("from", _options.From);
        _options.To.ForEach(to => queryString.Add("to", to));

        return PostAsync<object[], Responses.TextTranslations>(_options.TranslateEndpoint, queryString, request, cancellationToken);
    }
}
