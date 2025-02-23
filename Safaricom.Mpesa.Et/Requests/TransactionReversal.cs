using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// Mpesa Transaction reversal data transfer object
/// </summary>
public class TransactionReversal : MpesaRequest
{
    protected string? Type = nameof(TransactionReversal);
    /// <summary>
    /// The unique request ID for tracking a transaction
    /// </summary>
    public Guid? OriginatorConversationID { get; private set; }

    /// <summary>
    /// The name of Initiator to initiating  the request
    /// </summary>
    public string Initiator { get; private set; }

    /// <summary>
    /// Encrypted password for the initiator to authenticate the transaction request.
    /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
    /// </summary>
    public string SecurityCredential { get; private set; }

    /// <summary>
    /// Takes only 'TransactionReversal' Command id.
    /// The default value has been set to that so you don't have to set this property.
    /// </summary>
    public string CommandID { get; private set; } = TransactionType.TransactionReversal;

    /// <summary>
    /// Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234
    /// </summary>
    public string TransactionID { get; private set; }


    /// <summary>
    /// This is the Amount transacted, normally a numeric value. Money that customer pays to the Shorcode. 
    /// Only whole numbers are supported.
    /// </summary>
    [JsonIgnore]
    public int Amount { get; private set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Amount))]
    private string _amount => Amount.ToString();

    /// <summary>
    /// Organization receiving the transaction (Shortcode)
    /// </summary>
    public string ReceiverParty { get; private set; }

    /// <summary>
    /// Type of organization receiving the transaction.
    /// 11 - Organization Identifier on M-Pesa
    /// </summary>
    public string RecieverIdentifierType { get; private set; }

    /// <summary>
    /// The path that stores information of time out transaction.
    /// </summary>
    public string QueueTimeOutURL { get; private set; }

    /// <summary>
    /// The path that stores information of transaction 
    /// </summary>
    public string ResultURL { get; private set; }

    /// <summary>
    /// Comments that are sent along with the transaction. (Upto 100 characters)
    /// </summary>
    public string Remarks { get; private set; }

    /// <summary>
    /// Optional Parameter (upto 100 characters)
    /// </summary>
    public string Occasion { get; private set; }

    /// <summary>
    /// Transaction reversal data transfer object
    /// </summary>
    /// <param name="initiator">The name of Initiator to initiating  the request</param>
    /// <param name="securityCredential">
    /// Encrypted password for the initiator to authenticate the transaction request.
    /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
    /// </param>
    /// <param name="transactionId">Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234</param>
    /// <param name="amount">The amount used to pay for the transaction.</param>
    /// <param name="receiverparty">Organization receiving the transaction (Shortcode)</param>
    /// <param name="receiverIdentifierType">
    /// Type of organization receiving the transaction.
    /// 11 - Organization Identifier on M-Pesa
    /// </param>
    /// <param name="remarks">Comments that are sent along with the transaction. (Upto 100 characters)</param>
    /// <param name="queueTimeoutUrl">The path that stores information of time out transaction.</param>
    /// <param name="resultUrl">The path that stores information of transaction </param>
    /// <param name="occasion"> Optional Parameter (upto 100 characters)</param>
    public TransactionReversal(string initiator, string securityCredential, string transactionId, int amount, string receiverparty, string receiverIdentifierType, string queueTimeoutUrl, string resultUrl, string remarks, string occasion)
    {
        Initiator = initiator;
        SecurityCredential = securityCredential;
        CommandID = TransactionType.TransactionReversal;
        TransactionID = transactionId;
        Amount = amount;
        ReceiverParty = receiverparty;
        RecieverIdentifierType = receiverIdentifierType;
        Remarks = remarks;
        QueueTimeOutURL = queueTimeoutUrl;
        ResultURL = resultUrl;
        Occasion = occasion;
    }
    /// <summary>
    /// Transaction reversal data transfer object
    /// </summary>
    /// <param name="originatorConversationID">The unique request ID for tracking a transaction</param>
    /// <param name="initiator">The name of Initiator to initiating  the request</param>
    /// <param name="securityCredential">
    /// Encrypted password for the initiator to authenticate the transaction request.
    /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
    /// </param>
    /// <param name="transactionId">Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234</param>
    /// <param name="amount">The amount used to pay for the transaction.</param>
    /// <param name="receiverparty">Organization receiving the transaction (Shortcode)</param>
    /// <param name="receiverIdentifierType">
    /// Type of organization receiving the transaction.
    /// 11 - Organization Identifier on M-Pesa
    /// </param>
    /// <param name="remarks">Comments that are sent along with the transaction. (Upto 100 characters)</param>
    /// <param name="queueTimeoutUrl">The path that stores information of time out transaction.</param>
    /// <param name="resultUrl">The path that stores information of transaction </param>
    /// <param name="occasion"> Optional Parameter (upto 100 characters)</param>
    public TransactionReversal(Guid originatorConversationID, string initiator, string securityCredential, string transactionId, int amount, string receiverparty, string receiverIdentifierType, string queueTimeoutUrl, string resultUrl, string remarks, string occasion)
      : this(initiator, securityCredential, transactionId, amount, receiverparty, receiverIdentifierType, queueTimeoutUrl, resultUrl, remarks, occasion)
    {
        OriginatorConversationID = originatorConversationID;
    }
}