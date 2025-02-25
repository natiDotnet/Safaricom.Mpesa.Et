using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddTransient<LoggingHandler>()
            .AddTransient<TokenHandler>()
            .AddTransient<ValidationHandler>()
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
                string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                Console.WriteLine($"Current Environment: {environment}");
                string baseUrl = environment!.Equals("Development", StringComparison.OrdinalIgnoreCase)
                    ? "https://apisandbox.safaricom.et/"
                    : "https://api.safaricom.et/";
                config ??= sp.GetRequiredService<IOptions<MpesaConfig>>().Value;
                client.BaseAddress = new Uri(baseUrl);
			    
                return new MpesaClient(config, client);
            })
            .AddHttpMessageHandler<LoggingHandler>();
        return services;
    }
    
}
