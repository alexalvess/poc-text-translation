using System.ComponentModel.DataAnnotations;

namespace poc_translate.Services.HttpProxy;

public record TextTranslationHttpClientOptions
{
    [Required, Url]
    public string BaseAddress { get; init; }

    [Required]
    public string TranslateEndpoint { get; init; }

    [Required]
    public string Key { get; init; }

    [Required]
    public string Region { get; init; }

    [Required]
    public string ApiVersion { get; init; }

    [Required]
    public string From { get; init; }

    [Required]
    public List<string> To { get; init; }
}
