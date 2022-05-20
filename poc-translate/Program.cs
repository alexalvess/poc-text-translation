using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using poc_translate.Services;
using poc_translate.Services.HttpProxy;

var buider = Host
    .CreateDefaultBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();


buider.ConfigureServices(services =>
{
    services
        .AddOptions<TextTranslationHttpClientOptions>()
        .Bind(configuration.GetSection(nameof(TextTranslationHttpClientOptions)))
        .ValidateDataAnnotations();

    services
        .AddHttpClient<ITranslationHttpClient, TranslationHttpClient>()
        .ConfigureHttpClient((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<TextTranslationHttpClientOptions>>().Value;

            client.BaseAddress = new(options.BaseAddress);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", options.Key);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", options.Region);
        });

    services.AddScoped<ITranslationService, TranslationService>();
});


var app = buider.Build();

var service = app.Services.GetRequiredService<ITranslationService>();

var textToTranslate = @"
O Cruzeiro Esporte Clube é uma associação polidesportiva brasileira, 
com sede em Belo Horizonte, Minas Gerais. É considerado um dos maiores 
clubes de futebol do Brasil e da América do Sul";

var translatedText = await service.TranslateTextAsync(textToTranslate, default);

Console.WriteLine();
Console.WriteLine();
Console.WriteLine(translatedText.Trim());
Console.WriteLine();
Console.WriteLine();

await app.RunAsync();