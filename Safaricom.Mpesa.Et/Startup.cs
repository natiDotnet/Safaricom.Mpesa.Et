using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safaricom.Mpesa.Et.DelegatingHandlers;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et;

public static class Startup
{
    public static IServiceCollection AddMpesa(this IServiceCollection services, MpesaConfig? config = null)
    {
        if (config is null)
        {
            services.AddOptions<MpesaConfig>().BindConfiguration(MpesaConfig.Key);
        }
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        services.AddTransient<LoggingHandler>()
            .AddTransient<TokenHandler>(sp =>
            {
                config ??= sp.GetRequiredService<IOptions<MpesaConfig>>().Value;
                var cache = sp.GetRequiredService<IMemoryCache>();
                var encryptionService = sp.GetRequiredService<IEncryptionService>();
                var logger = sp.GetRequiredService<ILogger<TokenHandler>>();
                return new TokenHandler(config, environment, cache, encryptionService, logger);
            })
            .AddSingleton<IEncryptionService>(sp =>
            {
                config ??= sp.GetRequiredService<IOptions<MpesaConfig>>().Value;
                return new EncryptionService(config);
            })
            .AddMemoryCache();
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        services.AddHttpClient<IMpesaClient>()
            .AddTypedClient<IMpesaClient>((client, sp) =>
            {
                string baseUrl = environment!.Equals("Development", StringComparison.OrdinalIgnoreCase)
                    ? MpesaConfig.SandboxBaseUrl
                    : MpesaConfig.ProductionBaseUrl;
                config ??= sp.GetRequiredService<IOptions<MpesaConfig>>().Value;
                client.BaseAddress = new Uri(baseUrl);

                return new MpesaClient(config, client);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<TokenHandler>();
        return services;
    }

}
