using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
        client ??= new HttpClient();
        this.client = client;
    }
    public MpesaClient(string consumerKey, string consumerSecret)
       : this(new MpesaConfig { ConsumerKey = consumerKey, ConsumerSecret = consumerSecret })
    { }

    public async Task<AuthResponse?> AuthorizeAsync()
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", config.BasicAuth);
		var result = await client.GetAsync("v1/token/generate?grant_type=client_credentials")
                           .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<AuthErrorResponse>(Helper.SnakeCase);
            throw new MpesaAPIException(result.StatusCode, new MpesaErrorResponse { ErrorCode = error?.ResultCode, ErrorMessage = error?.ResultDesc });
        }
        return await result.Content.ReadFromJsonAsync<AuthResponse>(Helper.SnakeCase);
    }

    public async Task<MpesaResponse?> TransactionStatusAsync(TransactionStatus request)
    {
        var result = await client.PostAsJsonAsync("mpesa/transactionstatus/v1/query", request)
                                 .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            var error = await result.Content.ReadFromJsonAsync<MpesaErrorResponse>(Helper.SnakeCase);
            throw new MpesaAPIException(result.StatusCode, error!);
        }
        return await result.Content.ReadFromJsonAsync<MpesaResponse>(Helper.SnakeCase);
    }
}