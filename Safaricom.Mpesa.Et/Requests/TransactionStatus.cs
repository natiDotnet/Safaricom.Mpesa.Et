
using System.Text.Json.Serialization;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et.Requests;

public class TransactionStatus : MpesaRequest
{
    /// <summary>
    /// The name of Initiator to initiating  the request.
    /// This is the credential/username used to authenticate the transaction request.
    /// </summary>
    public required string Initiator { get; set; }

    /// <summary>
    /// Encrypted password for the initiator to authenticate the transaction request.
    /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
    /// </summary>
    public required string SecurityCredential { get; set; }

    /// <summary>
    /// Takes only 'TransactionStatusQuery' command id
    /// The default value has been set to that so you don't have to set this property.
    /// </summary>
    public readonly string CommandID = TransactionType.TransactionStatusQuery;

    /// <summary>
    /// Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234
    /// </summary>
    public required string TransactionID { get; set; }

    /// <summary>
    /// This is a global unique identifier for the transaction request returned by the API proxy upon successful request submission. If you don’t have the M-PESA transaction ID you can use this to query.
    /// </summary>
    public required string? OriginalConversationID { get; set; }

    /// <summary>
    /// Organization/MSISDN receiving the transaction
    /// </summary>
    public required string PartyA { get; set; }

    /// <summary>
    /// Type of organization receiving the transaction
    /// 1 – MSISDN
    /// 2 – Till Number
    /// 4 – Organization short code
    /// </summary>
    [JsonIgnore]
    public IdentifierType IdentifierType { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(IdentifierType))]
    private string identifierType => ((int)IdentifierType).ToString();

    /// <summary>
    /// The path that stores information of time out transaction. https://ip or domain:port/path
    /// </summary>
    public required Uri QueueTimeOutURL { get; set; }

    /// <summary>
    /// The path that stores information of transaction. https://ip or domain:port/path
    /// </summary>
    public required Uri ResultURL { get; set; }

    /// <summary>
    /// Comments that are sent along with the transaction
    /// </summary>
    public string Remarks { get; set; } = string.Empty;

    /// <summary>
    /// Optional Parameter. (upto 100 characters)
    /// </summary>
    public string Occasion { get; set; } = string.Empty;
}