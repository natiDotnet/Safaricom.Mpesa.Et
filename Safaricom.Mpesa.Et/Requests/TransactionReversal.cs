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
    /// <summary>
    /// The unique request ID for tracking a transaction
    /// </summary>
    public Guid? OriginatorConversationID { get; set; }

    /// <summary>
    /// The name of Initiator to initiating  the request
    /// </summary>
    public string Initiator { get; set; }

    /// <summary>
    /// Encrypted password for the initiator to authenticate the transaction request.
    /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
    /// </summary>
    public string SecurityCredential { get; set; }

    /// <summary>
    /// Takes only 'TransactionReversal' Command id.
    /// The default value has been set to that so you don't have to set this property.
    /// </summary>
    public string CommandID { get; set; } = TransactionType.TransactionReversal;

    /// <summary>
    /// Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234
    /// </summary>
    public string TransactionID { get; set; }


    /// <summary>
    /// This is the Amount transacted, normally a numeric value. Money that customer pays to the Shorcode. 
    /// Only whole numbers are supported.
    /// </summary>
    [JsonIgnore]
    public int Amount { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Amount))]
    private string _amount => Amount.ToString();

    /// <summary>
    /// Organization receiving the transaction (Shortcode)
    /// </summary>
    public string ReceiverParty { get; set; }

    /// <summary>
    /// Type of organization receiving the transaction.
    /// 11 - Organization Identifier on M-Pesa
    /// </summary>
    public string RecieverIdentifierType { get; set; }

    /// <summary>
    /// The path that stores information of time out transaction.
    /// </summary>
    public string QueueTimeOutURL { get; set; }

    /// <summary>
    /// The path that stores information of transaction 
    /// </summary>
    public string ResultURL { get; set; }

    /// <summary>
    /// Comments that are sent along with the transaction. (Upto 100 characters)
    /// </summary>
    public string Remarks { get; set; }

    /// <summary>
    /// Optional Parameter (upto 100 characters)
    /// </summary>
    public string Occasion { get; set; }
}