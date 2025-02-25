using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// AccountBalance data transfer object
/// </summary>
public class AccountBalance : MpesaRequest
{
    [JsonInclude]
    protected new string? Type = typeof(AccountBalance).FullName;
    /// <summary>
    /// This is the credential/username used to authenticate the transaction request.
    /// </summary>
    public string Initiator { get; set; }

    /// <summary>
    /// Base64 encoded string of the M-Pesa short code and password, which is encrypted using M-Pesa public key and validates the transaction on M-Pesa Core system.
    /// </summary>
    public string SecurityCredential { get; set; }

    /// <summary>
    /// A unique command passed to the M-Pesa system.
    /// </summary>
    public readonly string CommandID = TransactionType.AccountBalance;

    /// <summary>
    /// The shortcode of the organisation receiving the transaction.
    /// </summary>
    public string PartyA { get; set; }

    /// <summary>
    /// Type of the organisation receiving the transaction.
    /// </summary>
    [JsonIgnore]
    public IdentifierType IdentifierType { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(IdentifierType))]
    private string identifierType 
    { 
        get => ((int)IdentifierType).ToString();
        set => IdentifierType = (IdentifierType)Enum.Parse(typeof(IdentifierType), value); 
    }

    /// <summary>
    /// Comments that are sent along with the transaction.
    /// </summary>
    public string Remarks { get; set; }

    /// <summary>
    /// The timeout end-point that receives a timeout message.
    /// </summary>
    public string QueueTimeOutURL { get; set; }

    /// <summary>
    /// The end-point that receives a successful transaction.
    /// </summary>
    public string ResultURL { get; set; }

}