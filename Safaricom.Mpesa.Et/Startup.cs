using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Safaricom.Mpesa.Et;

public static class Startup
{
    public static IServiceCollection AddMpesa(this IServiceCollection services, MpesaConfig? config = null)
    {
        if (config is null)
        {
            services.AddOptions<MpesaConfig>().BindConfiguration(MpesaConfig.Key);
        }
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
			    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", config.BasicAuth);
			
                return new MpesaClient(config, client);
            });
        return services;
    }
    
}
