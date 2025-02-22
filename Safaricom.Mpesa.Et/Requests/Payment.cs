using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// B2C data transfer object
/// </summary>
public class Payment
{
    /// <summary>
    /// The unique request ID for tracking a transaction
    /// </summary>
    public Guid? OriginatorConversationID { get; private set; }

    /// <summary>
    /// The username of the M-Pesa B2C account API operator.
    /// NOTE: the access channel for this operator must be API and the account must be in active status.
    /// </summary>
    public string InitiatorName { get; private set; }

    /// <summary>
    /// This is the value obtained after encrypting the API initiator password.
    /// MpesaLib Provides the <c>Credentials.EncryptPassword</c> under MpesaLib.Helpers namespace to help you 
    /// with the encryption. You need a public certificate from Safaricom for either sandbox or live APIs for this to work.
    /// </summary>
    public string SecurityCredential { get; private set; }

    /// <summary>
    /// This is a unique command that specifies B2C transaction type.
    /// SalaryPayment: This supports sending money to both registered and unregistered M-Pesa customers.
    /// BusinessPayment: This is a normal business to customer payment, supports only M-Pesa registered customers.
    /// PromotionPayment: This is a promotional payment to customers.The M-Pesa notification message is a 
    /// congratulatory message. Supports only M-Pesa registered customers.
    /// </summary>
    public string CommandID { get; private set; }

    /// <summary>
    /// The amount of money being sent to the customer.
    /// </summary>
    [JsonIgnore]
    public decimal Amount { get; private set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Amount))]
    private string _amount => Amount.ToString("0.##");

    /// <summary>
    /// This is the B2C organization shortcode from which the money is to be sent.
    /// </summary>
    public string PartyA { get; private set; }

    /// <summary>
    /// This is the customer mobile number  to receive the amount.
    /// The number should have the country code (254) without the plus sign i.e 2547XXXXXXXX.
    /// </summary>
    public string PartyB { get; private set; }

    /// <summary>
    /// Any additional information to be associated with the transaction. (Upto 100 characters)
    /// </summary>
    public string Remarks { get; private set; }

    /// <summary>
    /// This is the URL to be specified in your request that will be used by API Proxy to send notification 
    /// incase the payment request is timed out while awaiting processing in the queue. 
    /// </summary>
    public string QueueTimeOutURL { get; private set; }

    /// <summary>
    /// This is the URL to be specified in your request that will be used by M-Pesa to send notification upon 
    /// processing of the payment request.
    /// </summary>
    public string ResultURL { get; private set; }

    /// <summary>
    /// Any additional information to be associated with the transaction. (Upto 100 characters)
    /// </summary>
    public string Occasion { get; private set; }


    /// <summary>
    /// B2C data transfer object
    /// </summary>
    /// <param name="initiatorName">
    /// The username of the M-Pesa B2C account API operator.
    /// NOTE: the access channel for this operator must be API and the account must be in active status.
    /// </param>
    /// <param name="securityCredential">
    /// This is the value obtained after encrypting the API initiator password.
    /// MpesaLib Provides the <c>Credentials.EncryptPassword</c> under MpesaLib.Helpers namespace to help you 
    /// with the encryption. You need a public certificate from Safaricom for either sandbox or live APIs for this to work.
    /// </param>
    /// <param name="commandId">
    /// This is a unique command that specifies B2C transaction type.
    /// SalaryPayment: This supports sending money to both registered and unregistered M-Pesa customers.
    /// BusinessPayment: This is a normal business to customer payment, supports only M-Pesa registered customers.
    /// PromotionPayment: This is a promotional payment to customers.The M-Pesa notification message is a 
    /// congratulatory message. Supports only M-Pesa registered customers.
    /// </param>
    /// <param name="amount">The amount of money being sent to the customer.</param>
    /// <param name="partyA">This is the B2C organization shortcode from which the money is to be sent.</param>
    /// <param name="partyB">
    /// This is the customer mobile number  to receive the amount.
    /// The number should have the country code (251) without the plus sign i.e 2517XXXXXXXX.
    /// </param>
    /// <param name="remarks">Any additional information to be associated with the transaction. (Upto 100 characters)</param>
    /// <param name="occasion">Any additional information to be associated with the transaction. (Upto 100 characters)</param>
    /// <param name="queueTimeoutUrl">
    /// This is the URL to be specified in your request that will be used by API Proxy to send notification 
    /// incase the payment request is timed out while awaiting processing in the queue. 
    /// </param>
    /// <param name="resultUrl">
    /// This is the URL to be specified in your request that will be used by M-Pesa to send notification upon 
    /// processing of the payment request.
    /// </param>
    public Payment(string initiatorName, string securityCredential, string commandId, decimal amount, string partyA, string partyB, string remarks, string occasion, string queueTimeoutUrl, string resultUrl)
    {
        InitiatorName = initiatorName;
        SecurityCredential = securityCredential;
        CommandID = commandId;
        Amount = amount;
        PartyA = partyA;
        PartyB = partyB;
        Remarks = remarks;
        Occasion = occasion;
        QueueTimeOutURL = queueTimeoutUrl;
        ResultURL = resultUrl;
    }

    /// <summary>
    /// B2C data transfer object
    /// </summary>
    /// <param name="originatorConversationID">The unique request ID for tracking a transaction</param>
    /// <param name="initiatorName">
    /// The username of the M-Pesa B2C account API operator.
    /// NOTE: the access channel for this operator must be API and the account must be in active status.
    /// </param>
    /// <param name="securityCredential">
    /// This is the value obtained after encrypting the API initiator password.
    /// MpesaLib Provides the <c>Credentials.EncryptPassword</c> under MpesaLib.Helpers namespace to help you 
    /// with the encryption. You need a public certificate from Safaricom for either sandbox or live APIs for this to work.
    /// </param>
    /// <param name="commandId">
    /// This is a unique command that specifies B2C transaction type.
    /// SalaryPayment: This supports sending money to both registered and unregistered M-Pesa customers.
    /// BusinessPayment: This is a normal business to customer payment, supports only M-Pesa registered customers.
    /// PromotionPayment: This is a promotional payment to customers.The M-Pesa notification message is a 
    /// congratulatory message. Supports only M-Pesa registered customers.
    /// </param>
    /// <param name="amount">The amount of money being sent to the customer.</param>
    /// <param name="partyA">This is the B2C organization shortcode from which the money is to be sent.</param>
    /// <param name="partyB">
    /// This is the customer mobile number  to receive the amount.
    /// The number should have the country code (251) without the plus sign i.e 2517XXXXXXXX.
    /// </param>
    /// <param name="remarks">Any additional information to be associated with the transaction. (Upto 100 characters)</param>
    /// <param name="occasion">Any additional information to be associated with the transaction. (Upto 100 characters)</param>
    /// <param name="queueTimeoutUrl">
    /// This is the URL to be specified in your request that will be used by API Proxy to send notification 
    /// incase the payment request is timed out while awaiting processing in the queue. 
    /// </param>
    /// <param name="resultUrl">
    /// This is the URL to be specified in your request that will be used by M-Pesa to send notification upon 
    /// processing of the payment request.
    /// </param>
    public Payment(Guid originatorConversationID, string initiatorName, string securityCredential, string commandId, decimal amount, string partyA, string partyB, string remarks, string occasion, string queueTimeoutUrl, string resultUrl)
        : this(initiatorName, securityCredential, commandId, amount, partyA, partyB, remarks, occasion, queueTimeoutUrl, resultUrl)
    {
        OriginatorConversationID = originatorConversationID;
    }
}