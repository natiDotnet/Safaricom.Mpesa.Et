using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// C2B Register URLs data transfer object
/// </summary>
public class C2BRegisterUrl
{
    /// <summary>
    /// The short code of the organization. 
    /// </summary>      
    public string ShortCode { get; private set; }

    /// <summary>
    /// This parameter specifies what is to happen if for any reason the validation URL is nor reachable. 
    /// Note that, This is the default action value that determines what MPesa will do in the scenario that your
    /// endpoint is unreachable or is unable to respond on time. 
    /// Only two values are allowed: Completed or Cancelled. 
    /// Completed means MPesa will automatically complete your transaction, in the event MPesa is unable to 
    /// reach your Validation URL 
    /// Cancelled means MPesa will automatically cancel the transaction, in the event MPesa is unable to 
    /// reach your Validation URL.
    /// </summary>
    [JsonIgnore]
    public ResponseType ResponseType { get; private set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(ResponseType))]
    private string _responseType => ResponseType.ToString();
    

    /// <summary>
    /// A unique command passed to the M-Pesa system.
    /// </summary>
    public readonly string CommandID = TransactionType.RefisterUrl;
    /// <summary>
    /// This is the URL that receives the confirmation request from API upon payment completion.
    /// </summary>
    public string ConfirmationURL { get; private set; }

    /// <summary>
    /// This is the URL that receives the validation request from API upon payment submission. 
    /// The validation URL is only called if the external validation on the registered shortcode is enabled. 
    /// (By default External Validation is dissabled, contact MPESA API team if you want this enbaled for your app)
    /// </summary>
    public string ValidationURL { get; private set; }

    /// <summary>
    /// C2B Register URLs data transfer object
    /// </summary>
    /// <param name="shortCode">The short code of the organization. </param>
    /// <param name="responseType">
    /// This parameter specifies what is to happen if for any reason the validation URL is nor reachable. 
    /// Note that, This is the default action value that determines what MPesa will do in the scenario that your
    /// endpoint is unreachable or is unable to respond on time. 
    /// Only two values are allowed: Completed or Cancelled. 
    /// Completed means MPesa will automatically complete your transaction, in the event MPesa is unable to 
    /// reach your Validation URL 
    /// Cancelled means MPesa will automatically cancel the transaction, in the event MPesa is unable to 
    /// reach your Validation URL.
    /// </param>
    /// <param name="confirmationUrl">This is the URL that receives the confirmation request from API upon payment completion.</param>
    /// <param name="validationUrl">
    /// This is the URL that receives the validation request from API upon payment submission. 
    /// The validation URL is only called if the external validation on the registered shortcode is enabled. 
    /// (By default External Validation is dissabled, contact MPESA API team if you want this enbaled for your app)
    /// </param>
    public C2BRegisterUrl(string shortCode, ResponseType responseType, string confirmationUrl, string validationUrl)
    {
        ShortCode = shortCode;
        ResponseType = responseType;
        ConfirmationURL = confirmationUrl;
        ValidationURL = validationUrl;
    }
}

public enum ResponseType
{
    Completed,
    Cancelled
}