using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Safaricom.Mpesa.Et.Responses;

namespace Safaricom.Mpesa.Et.Exceptions;

/// <summary>
/// Mpesa Api Exception Custom class
/// </summary>
public class MpesaAPIException : Exception
{
    /// <summary>
    /// Http Status code retrieved from the Mpesa API call
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }


    /// <summary>
    /// Mpesa Error Response object
    /// </summary>
    public MpesaErrorResponse? MpesaErrorResponse { get; set; }

    public MpesaAPIException(HttpStatusCode statusCode, string message) : base(message)
     => StatusCode = statusCode;
    
    public MpesaAPIException(HttpStatusCode statusCode, MpesaErrorResponse mpesaErrorResponse) : base(mpesaErrorResponse.ErrorMessage)
    {
        StatusCode = statusCode;
        MpesaErrorResponse = mpesaErrorResponse;
    }
    public MpesaAPIException(Exception ex, HttpStatusCode statusCode, MpesaErrorResponse mpesaErrorResponse) : base (ex.Message)
    {
        StatusCode = statusCode;
        MpesaErrorResponse = mpesaErrorResponse;
    }
}
