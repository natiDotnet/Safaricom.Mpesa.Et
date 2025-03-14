using System.Text.Json.Serialization;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// B2C data transfer object
/// </summary>
public class Payment
{
    /// <summary>
    /// The unique request ID for tracking a transaction
    /// </summary>
    public required string OriginatorConversationID { get; set; }

    /// <summary>
    /// The username of the M-Pesa B2C account API operator.
    /// NOTE: the access channel for this operator must be API and the account must be in active status.
    /// </summary>
    public required string InitiatorName { get; set; }

    /// <summary>
    /// This is the value obtained after encrypting the API initiator password.
    /// MpesaLib Provides the <c>Credentials.EncryptPassword</c> under MpesaLib.Helpers namespace to help you 
    /// with the encryption. You need a public certificate from Safaricom for either sandbox or live APIs for this to work.
    /// </summary>
    public required string SecurityCredential { get; set; }

    /// <summary>
    /// This is a unique command that specifies B2C transaction type.
    /// SalaryPayment: This supports sending money to both registered and unregistered M-Pesa customers.
    /// BusinessPayment: This is a normal business to customer payment, supports only M-Pesa registered customers.
    /// PromotionPayment: This is a promotional payment to customers.The M-Pesa notification message is a 
    /// congratulatory message. Supports only M-Pesa registered customers.
    /// </summary>
    [JsonIgnore]
    public required PaymentType CommandID { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(CommandID))]
    private string commandID => CommandID.ToString();

    /// <summary>
    /// The amount of money being sent to the customer.
    /// </summary>
    //[JsonIgnore]
    public required int Amount { get; set; }

    //[JsonInclude]
    //[JsonPropertyName(nameof(Amount))]
    //private string amount => Amount.ToString("0.##");

    /// <summary>
    /// This is the B2C organization shortcode from which the money is to be sent.
    /// </summary>
    public required short PartyA { get; set; }

    /// <summary>
    /// This is the customer mobile number  to receive the amount.
    /// The number should have the country code (254) without the plus sign i.e 2547XXXXXXXX.
    /// </summary>
    public required string PartyB { get; set; }

    /// <summary>
    /// Any additional information to be associated with the transaction. (Upto 100 characters)
    /// </summary>
    public required string Remarks { get; set; }

    /// <summary>
    /// This is the URL to be specified in your request that will be used by API Proxy to send notification 
    /// incase the payment request is timed out while awaiting processing in the queue. 
    /// </summary>
    public required Uri QueueTimeOutURL { get; set; }

    /// <summary>
    /// This is the URL to be specified in your request that will be used by M-Pesa to send notification upon 
    /// processing of the payment request.
    /// </summary>
    public required Uri ResultURL { get; set; }

    /// <summary>
    /// Any additional information to be associated with the transaction. (Upto 100 characters)
    /// </summary>
    public string Occasion { get; set; } = string.Empty;
}

public enum PaymentType
{
    SalaryPayment,
    BusinessPayment,
    PromotionPayment
}