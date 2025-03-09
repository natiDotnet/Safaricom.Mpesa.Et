using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Safaricom.Mpesa.Et.DelegatingHandlers;

public class LoggingHandler : DelegatingHandler
{
    private readonly ILogger<LoggingHandler> _logger;

    public LoggingHandler(ILogger<LoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request: {await request.Content?.ReadAsStringAsync()!}");
        var response = await base.SendAsync(request, cancellationToken);
        _logger.LogInformation($"Response: {response}");
        return response;
    }

}
