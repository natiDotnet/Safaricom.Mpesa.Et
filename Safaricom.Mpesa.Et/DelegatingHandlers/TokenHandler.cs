using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Safaricom.Mpesa.Et;
using Safaricom.Mpesa.Et.Shared;

public class TokenHandler : DelegatingHandler
{
    private readonly IMpesaClient _mpesa;
    private readonly IMemoryCache _cache;
    private readonly IEncryptionService _encryptionService;
    private readonly ILogger<TokenHandler> _logger;
    private static readonly SemaphoreSlim _lock = new(1, 1);

    public TokenHandler(IMpesaClient mpesa, IMemoryCache cache, IEncryptionService encryptionService, ILogger<TokenHandler> logger)
    {
        _mpesa = mpesa;
        _cache = cache;
        _encryptionService = encryptionService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue("AccessToken", out string? token))
        {
            return _encryptionService.Decrypt(token);
        }

        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (_cache.TryGetValue("AccessToken", out token))
            {
                return _encryptionService.Decrypt(token);
            }

            _logger.LogInformation("Fetching a new access token...");
            var newToken = await _mpesa.AuthorizeAsync(cancellationToken);
            _cache.Set("AccessToken", _encryptionService.Encrypt(newToken.AccessToken), TimeSpan.FromSeconds(int.Parse(newToken.ExpiresIn) - 10)); // Store until expiration
            return newToken.AccessToken;
        }
        finally
        {
            _lock.Release();
        }
    }
}
