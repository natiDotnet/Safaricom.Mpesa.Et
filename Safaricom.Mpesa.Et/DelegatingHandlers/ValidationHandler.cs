using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Safaricom.Mpesa.Et.Exceptions;
using Safaricom.Mpesa.Et.Responses;

namespace Safaricom.Mpesa.Et.DelegatingHandlers
{
    /// <summary>
    /// Delegating Handler for request validation using FluentValidation.
    /// </summary>
    public class ValidationHandler : DelegatingHandler
    {
        private readonly ILogger<ValidationHandler> _logger;
        private readonly IServiceProvider _serviceProvider;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ValidationHandler(ILogger<ValidationHandler> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing request: {RequestMethod} {RequestUri}", request.Method, request.RequestUri);

            if (request.Content is not null)
            {
                var validationResult = await ValidateRequestAsync(request, cancellationToken);
                if (validationResult is not null)
                {
                    return validationResult;
                }
            }

            var response = await base.SendAsync(request, cancellationToken);
            _logger.LogInformation("Response received: {StatusCode}", response.StatusCode);

            return response;
        }

        private async Task<HttpResponseMessage?> ValidateRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var requestObject = await request.Content?.ReadFromJsonAsync<JsonNode>(_jsonOptions, cancellationToken)!;
                if (requestObject is null)
                {
                    return CreateBadRequest("Missing or invalid 'Type' property.");
                }

                var typeString = requestObject["Type"]?.GetValue<string>();
                if (string.IsNullOrWhiteSpace(typeString))
                {
                    return CreateBadRequest("The 'Type' property cannot be empty.");
                }

                var type = Type.GetType(typeString);
                if (type is null)
                {
                    return CreateBadRequest($"Invalid request type '{typeString}'.");
                }

                var requestBody = JsonSerializer.Deserialize(requestObject.ToString(), type, _jsonOptions);
                if (requestBody is null)
                {
                    return CreateBadRequest("Invalid request body format.");
                }

                var validationResult = await ValidateObjectAsync(requestBody, type, cancellationToken);
                if (validationResult is not null)
                {
                    return validationResult;
                }
            }
            catch (JsonException ex)
            {
                _logger.LogWarning(ex, "JSON deserialization error.");
                return CreateBadRequest("Invalid JSON format.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during request validation.");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = JsonContent.Create(new MpesaErrorResponse
                    {
                        ErrorCode = HttpStatusCode.InternalServerError.ToString(),
                        ErrorMessage = "An internal server error occurred."
                    })
                };
            }

            return null;
        }

        private async Task<HttpResponseMessage?> ValidateObjectAsync(object requestBody, Type requestType, CancellationToken cancellationToken)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(requestType);
            var validator = _serviceProvider.GetService(validatorType) as IValidator;
            if (validator is null)
            {
                return null; // No validator found, continue processing
            }

            var validationContextType = typeof(ValidationContext<>).MakeGenericType(requestType);
            var validationContext = Activator.CreateInstance(validationContextType, requestBody);

            var validationResultTask = (Task<ValidationResult>)validatorType
                .GetMethod("ValidateAsync")?
                .Invoke(validator, new object[] { validationContext!, cancellationToken })!;

            var validationResult = await validationResultTask;
            if (!validationResult.IsValid)
            {
                var errorResponse = new MpesaErrorResponse
                {
                    ErrorCode = HttpStatusCode.BadRequest.ToString(),
                    ErrorMessage = validationResult.Errors[0].ErrorMessage
                };

                throw new MpesaAPIException(HttpStatusCode.BadRequest, errorResponse);
            }

            return null;
        }

        private static HttpResponseMessage CreateBadRequest(string errorMessage)
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = JsonContent.Create(new MpesaErrorResponse
                {
                    ErrorCode = HttpStatusCode.BadRequest.ToString(),
                    ErrorMessage = errorMessage
                })
            };
        }
    }
}