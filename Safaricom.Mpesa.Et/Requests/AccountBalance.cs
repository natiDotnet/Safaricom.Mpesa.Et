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
    /// This is the credential/username used to authenticate the transaction request.
    /// </summary>
    public string Initiator { get; private set; }

    /// <summary>
    /// Base64 encoded string of the M-Pesa short code and password, which is encrypted using M-Pesa public key and validates the transaction on M-Pesa Core system.
    /// </summary>
    public string SecurityCredential { get; private set; }

    /// <summary>
    /// A unique command passed to the M-Pesa system.
    /// </summary>
    public string CommandID { get; private set; } = TransactionType.AccountBalance;

    /// <summary>
    /// The shortcode of the organisation receiving the transaction.
    /// </summary>
    public string PartyA { get; private set; }

    /// <summary>
    /// Type of the organisation receiving the transaction.
    /// </summary>
    [JsonIgnore]
    public IdentifierType IdentifierType { get; private set; }

    [JsonInclude]
    [JsonPropertyName("IdentifierType")]
    private string IdentifierTypeString => IdentifierType.ToString();

    /// <summary>
    /// Comments that are sent along with the transaction.
    /// </summary>
    public string Remarks { get; private set; }

    /// <summary>
    /// The timeout end-point that receives a timeout message.
    /// </summary>
    public string QueueTimeOutURL { get; private set; }

    /// <summary>
    /// The end-point that receives a successful transaction.
    /// </summary>
    public string ResultURL { get; private set; }

    /// <summary>
    /// Accountbalance data transfer object
    /// </summary>
    /// <param name="initiator">
    /// This is the credential/username used to authenticate the transaction request.
    /// </param>
    /// <param name="securityCredential">
    /// Base64 encoded string of the Security Credential, which is encrypted using M-Pesa public key and validates the transaction on M-Pesa Core system.
    /// </param>
    /// <param name="partyA">The shortcode of the organisation receiving the transaction.</param>
    /// <param name="identifierType">Type of the organisation receiving the transaction.</param>
    /// <param name="remarks">Comments that are sent along with the transaction.</param>
    /// <param name="queueTimeoutUrl">The timeout end-point that receives a timeout message.</param>
    /// <param name="resultUrl">The end-point that receives a successful transaction.</param>
    public AccountBalance(string initiator, string securityCredential, string partyA, IdentifierType identifierType, string remarks, string queueTimeoutUrl, string resultUrl)
    {
        Initiator = initiator;
        SecurityCredential = securityCredential;
        CommandID = TransactionType.AccountBalance;
        PartyA = partyA;
        IdentifierType = identifierType;
        Remarks = remarks;
        QueueTimeOutURL = queueTimeoutUrl;
        ResultURL = resultUrl;
    }
}