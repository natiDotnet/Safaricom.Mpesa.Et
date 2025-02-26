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
public class AccountBalance
{
    /// <summary>
    /// The unique request ID for tracking a transaction.
    /// </summary>
    public required Guid OriginatorConversationID { get; set; }
    /// <summary>
    /// This is the credential/username used to authenticate the transaction request.
    /// </summary>
    public required string Initiator { get; set; }

    /// <summary>
    /// Base64 encoded string of the M-Pesa short code and password, which is encrypted using M-Pesa public key and validates the transaction on M-Pesa Core system.
    /// </summary>
    public required string SecurityCredential { get; set; }

    /// <summary>
    /// A unique command passed to the M-Pesa system.
    /// </summary>
    public readonly string CommandID = TransactionType.AccountBalance;

    /// <summary>
    /// The shortcode of the organisation receiving the transaction.
    /// </summary>
    public required string PartyA { get; set; }

    /// <summary>
    /// Type of the organisation receiving the transaction.
    /// </summary>
    [JsonIgnore]
    public IdentifierType IdentifierType { get; set; } = IdentifierType.Organization;

    [JsonInclude]
    [JsonPropertyName(nameof(IdentifierType))]
    private string identifierType => ((int)IdentifierType).ToString();

    /// <summary>
    /// Comments that are sent along with the transaction.
    /// </summary>
    public required string Remarks { get; set; }

    /// <summary>
    /// The timeout end-point that receives a timeout message.
    /// </summary>
    public required Uri QueueTimeOutURL { get; set; }

    /// <summary>
    /// The end-point that receives a successful transaction.
    /// </summary>
    public required Uri ResultURL { get; set; }

}