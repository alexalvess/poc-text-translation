using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace poc_translate.Abstractions.Http;

public abstract class ApplicationHttpClient
{
    private readonly HttpClient _httpClient;

    protected ApplicationHttpClient(HttpClient httpClient)
        => _httpClient = httpClient;

    protected Task<HttpResponse<List<TResponse>>> PostAsync<TRequest, TResponse>(string endpoint, Dictionary<string, string> queryParameters, TRequest request, CancellationToken cancellationToken)
            where TResponse : new()
            => RequestAsync<TResponse>((client, ct) => client.PostAsJsonAsync(QueryHelpers.AddQueryString(endpoint, queryParameters ?? new()), request, ct), cancellationToken);

    private async Task<HttpResponse<List<TResponse>>> RequestAsync<TResponse>(Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> requestAsync, CancellationToken cancellationToken)
            where TResponse : new()
    {
        var response = await requestAsync(_httpClient, cancellationToken);

        return new()
        {
            Success = response.IsSuccessStatusCode,
            Content = response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<List<TResponse>>(cancellationToken: cancellationToken)
                : new()
        };
    }
}
