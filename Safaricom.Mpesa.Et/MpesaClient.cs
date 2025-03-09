using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Safaricom.Mpesa.Et.Exceptions;
using Safaricom.Mpesa.Et.Requests;
using Safaricom.Mpesa.Et.Responses;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et;

public class MpesaClient : IMpesaClient
{
    private readonly HttpClient client;
    private readonly MpesaConfig config;

    public MpesaClient(MpesaConfig config, HttpClient? client = null)
    {
        this.config = config;
        var services = new ServiceCollection().AddMpesa(config);
        client ??= new HttpClient();
        // services.BuildServiceProvider()
        //                   .GetRequiredService<IHttpClientFactory>()
        //                   .CreateClient("mpesa");
        this.client = client;
    }
    public MpesaClient(string consumerKey, string consumerSecret)
       : this(new MpesaConfig { ConsumerKey = consumerKey, ConsumerSecret = consumerSecret })
    { }

    public async Task<MpesaResponse?> AccountBalanceAsync(AccountBalance request, CancellationToken cancellationToken = default)
    {
        var result = await client.PostAsJsonAsync("mpesa/accountbalance/v2/query", request, Helper.PascalCase, cancellationToken)
                                 .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<MpesaErrorResponse>(cancellationToken);
            throw new MpesaAPIException(result.StatusCode, error!);
        }
        return await result.Content.ReadFromJsonAsync<MpesaResponse>(Helper.PascalCase, cancellationToken);
    }

    public async Task<AuthResponse?> AuthorizeAsync(CancellationToken cancellationToken = default)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", config.BasicAuth);
        var result = await client.GetAsync("v1/token/generate?grant_type=client_credentials", cancellationToken)
                           .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<AuthErrorResponse>(cancellationToken);
            throw new MpesaAPIException(result.StatusCode, new MpesaErrorResponse { ErrorCode = error?.ResultCode, ErrorMessage = error?.ResultDesc });
        }
        return await result.Content.ReadFromJsonAsync<AuthResponse>(Helper.SnakeCase, cancellationToken);
    }

    public async Task<MpesaResponse?> C2BSimulateAsync(C2BSimulate request, CancellationToken cancellationToken = default)
    {
        var result = await client.PostAsJsonAsync("mpesa/c2b/v1/simulate", request, Helper.PascalCase, cancellationToken)
                            .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<MpesaErrorResponse>(cancellationToken);
            throw new MpesaAPIException(result.StatusCode, error!);
        }
        return await result.Content.ReadFromJsonAsync<MpesaResponse>(Helper.PascalCase, cancellationToken);
    }

    public async Task<MpesaResponse?> PayoutAsync(Payment request, CancellationToken cancellationToken = default)
    {
        var result = await client.PostAsJsonAsync("mpesa/b2c/v1/paymentrequest", request, Helper.PascalCase, cancellationToken)
                           .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<MpesaErrorResponse>(cancellationToken);
            throw new MpesaAPIException(result.StatusCode, error!);
        }
        return await result.Content.ReadFromJsonAsync<MpesaResponse>(Helper.PascalCase, cancellationToken);
    }

    public async Task<MpesaResponse?> RegisterUrlAsync(string userName, C2BRegister request, CancellationToken cancellationToken = default)
    {
        var result = await client.PostAsJsonAsync($"v1/c2b-register-url/register?apikey={userName}", request, Helper.PascalCase, cancellationToken)
                                 .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<MpesaErrorResponse>(cancellationToken);
            throw new MpesaAPIException(result.StatusCode, error!);
        }
        return await result.Content.ReadFromJsonAsync<MpesaResponse>(Helper.PascalCase, cancellationToken);
    }

    public async Task<MpesaResponse?> ReverseTransactionAsync(TransactionReversal request, CancellationToken cancellationToken = default)
    {
        var result = await client.PostAsJsonAsync("mpesa/reversal/v2/request", request, Helper.PascalCase, cancellationToken)
                                 .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<MpesaErrorResponse>(cancellationToken);
            throw new MpesaAPIException(result.StatusCode, error!);
        }
        return await result.Content.ReadFromJsonAsync<MpesaResponse>(Helper.PascalCase, cancellationToken);
    }

    public async Task<MpesaResponse?> TransactionStatusAsync(TransactionStatus request, CancellationToken cancellationToken = default)
    {
        var result = await client.PostAsJsonAsync("mpesa/transactionstatus/v1/query", request, Helper.PascalCase, cancellationToken)
                                 .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<MpesaErrorResponse>(cancellationToken);
            throw new MpesaAPIException(result.StatusCode, error!);
        }
        return await result.Content.ReadFromJsonAsync<MpesaResponse>(Helper.PascalCase, cancellationToken);
    }

    public async Task<MpesaResponse?> UssdPushAsync(CheckoutOnline request, CancellationToken cancellationToken = default)
    {
        var result = await client.PostAsJsonAsync("mpesa/stkpush/v3/processrequest", request, Helper.PascalCase, cancellationToken)
                                 .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<MpesaErrorResponse>(cancellationToken);
            throw new MpesaAPIException(result.StatusCode, error!);
        }
        return await result.Content.ReadFromJsonAsync<MpesaResponse>(Helper.PascalCase, cancellationToken);
    }
}