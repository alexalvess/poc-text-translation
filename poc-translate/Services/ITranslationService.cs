namespace poc_translate.Services;

public interface ITranslationService
{
    Task<string> TranslateTextAsync(string toTranslate, CancellationToken cancellationToken);
}
