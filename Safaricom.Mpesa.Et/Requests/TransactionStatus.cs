
using System.Text.Json.Serialization;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et.Requests;

public class TransactionStatus : MpesaRequest
{
    protected string? Type = nameof(TransactionStatus);
    /// <summary>
    /// The name of Initiator to initiating  the request.
    /// This is the credential/username used to authenticate the transaction request.
    /// </summary>
    public string Initiator { get; private set; }

    /// <summary>
    /// Encrypted password for the initiator to authenticate the transaction request.
    /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
    /// </summary>
    public string SecurityCredential { get; private set; }

    /// <summary>
    /// Takes only 'TransactionStatusQuery' command id
    /// The default value has been set to that so you don't have to set this property.
    /// </summary>
    public string CommandID { get; private set; } = TransactionType.TransactionStatusQuery;

    /// <summary>
    /// Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234
    /// </summary>
    public string TransactionID { get; private set; }

    /// <summary>
    /// This is a global unique identifier for the transaction request returned by the API proxy upon successful request submission. If you don’t have the M-PESA transaction ID you can use this to query.
    /// </summary>
    public string? OriginatorConversationID { get; private set; }

    /// <summary>
    /// Organization/MSISDN receiving the transaction
    /// </summary>
    public string PartyA { get; private set; }

    /// <summary>
    /// Type of organization receiving the transaction
    /// 1 – MSISDN
    /// 2 – Till Number
    /// 4 – Organization short code
    /// </summary>
    [JsonIgnore]
    public IdentifierType IdentifierType { get; private set; }

    [JsonInclude]
    [JsonPropertyName("identifierType")]
    private string IdentifierTypeString { get => this.IdentifierType.ToString(); }

    /// <summary>
    /// The path that stores information of time out transaction. https://ip or domain:port/path
    /// </summary>
    public string QueueTimeOutURL { get; private set; }

    /// <summary>
    /// The path that stores information of transaction. https://ip or domain:port/path
    /// </summary>
    public string ResultURL { get; private set; }

    /// <summary>
    /// Comments that are sent along with the transaction
    /// </summary>
    public string? Remarks { get; private set; }

    /// <summary>
    /// Optional Parameter. (upto 100 characters)
    /// </summary>
    public string? Occasion { get; private set; }

    /// <summary>
    /// Mpesa Transaction Status Query data transfer object
    /// </summary>
    /// <param name="initiator">
    /// The name of Initiator to initiating  the request.
    /// This is the credential/username used to authenticate the transaction request.
    /// </param>
    /// <param name="securityCredential">
    /// Encrypted password for the initiator to authenticate the transaction request.
    /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
    /// </param>
    /// <param name="transactionId">Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234</param>
    /// <param name="partyA">Organization/MSISDN receiving the transaction</param>
    /// <param name="identifierType">
    /// Type of organization receiving the transaction
    /// 1 – MSISDN
    /// 2 – Till Number
    /// 4 – Organization short code
    /// <param name="queueTimeoutUrl">The path that stores information of time out transaction. https://ip or domain:port/path</param>
    /// <param name="resultUrl">The path that stores information of transaction. https://ip or domain:port/path</param>
    /// </param>
    /// <param name="remarks">
    /// Comments that are sent along with the transaction
    /// </param>
    /// <param name="occasion">Optional Parameter. (upto 100 characters)</param>
    public TransactionStatus(string initiator, string securityCredential, string transactionId, string partyA, IdentifierType identifierType, string queueTimeoutUrl, string resultUrl, string? remarks = null, string? occasion = null)
    {
        Initiator = initiator;
        SecurityCredential = securityCredential;
        CommandID = TransactionType.TransactionStatusQuery;
        TransactionID = transactionId;
        PartyA = partyA;
        IdentifierType = identifierType;
        QueueTimeOutURL = queueTimeoutUrl;
        ResultURL = resultUrl;
        Remarks = remarks;
        Occasion = occasion;
    }

    /// <summary>
    /// Mpesa Transaction Status Query data transfer object
    /// </summary>
    /// <param name="initiator">
    /// The name of Initiator to initiating  the request.
    /// This is the credential/username used to authenticate the transaction request.
    /// </param>
    /// <param name="securityCredential">
    /// Encrypted password for the initiator to authenticate the transaction request.
    /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
    /// </param>
    /// <param name="transactionId">Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234</param>
    /// <param name="originatorConversationID">Unique identifier to identify without transaction ID. e.g AG_XXXXX_XXX</param>
    /// <param name="partyA">Organization/MSISDN receiving the transaction</param>
    /// <param name="identifierType">
    /// Type of organization receiving the transaction
    /// 1 – MSISDN
    /// 2 – Till Number
    /// 4 – Organization short code
    /// </param>
    /// <param name="queueTimeoutUrl">The path that stores information of time out transaction. https://ip or domain:port/path</param>
    /// <param name="resultUrl">The path that stores information of transaction. https://ip or domain:port/path</param>
    /// <param name="remarks">
    /// Comments that are sent along with the transaction
    /// </param>
    /// <param name="occasion">Optional Parameter. (upto 100 characters)</param>
    public TransactionStatus(string initiator, string securityCredential, string transactionId, string originatorConversationID, string partyA, IdentifierType identifierType, string queueTimeoutUrl, string resultUrl, string? remarks, string? occasion)
    {
        Initiator = initiator;
        SecurityCredential = securityCredential;
        CommandID = TransactionType.TransactionStatusQuery;
        TransactionID = transactionId;
        OriginatorConversationID = originatorConversationID;
        PartyA = partyA;
        IdentifierType = identifierType;
        Remarks = remarks;
        QueueTimeOutURL = queueTimeoutUrl;
        ResultURL = resultUrl;
        Occasion = occasion;
    }
}