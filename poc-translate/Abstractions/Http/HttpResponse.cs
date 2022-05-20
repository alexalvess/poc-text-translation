namespace poc_translate.Abstractions.Http;

public record HttpResponse<TContent>
{
    public bool Success { get; init; }

    public TContent Content { get; init; }
}
