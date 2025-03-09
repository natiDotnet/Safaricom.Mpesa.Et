using System.Globalization;
using System.Text.Json.Serialization;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// Represents a C2B Validation request received from M-Pesa.
/// </summary>
public class C2BValidation
{
    /// <summary>
    /// Indicates the type of request. Expected value is "Validation" or "Confirmation".
    /// </summary>
    public RequestType RequestType;

    /// <summary>
    /// The transaction type specified during the payment request.
    /// </summary>
    [JsonIgnore]
    public required C2BTransactionType TransactionType { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TransactionType))]
    private string transactionType => TransactionType.ToString().Humanize();


    /// <summary>
    /// This is the unique M-Pesa transaction ID for every payment request.
    /// This is sent to both the call-back messages and a confirmation SMS sent to the customer.
    /// </summary>
    public required string TransID { get; set; }

    /// <summary>
    /// This is the Timestamp of the transaction, normally in the format of YEAR+MONTH+DATE+HOUR+MINUTE+SECOND (YYYYMMDDHHMMSS) 
    /// Each part should be at least two digits apart from the year which takes four digits.
    /// </summary>
    [JsonIgnore]
    public required TimeSpan TransTime { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TransTime))]
    private string transTime => TransTime.ToString("yyyyMMddHHmmss");

    /// <summary>
    /// This is the amount transacted (normally a numeric value), money that the customer pays to the Shortcode. Only whole numbers are supported.
    /// </summary>
    public required int TransAmount { get; set; }

    /// <summary>
    /// This is the organization's shortcode (Paybill or Buygoods - a 5 to 6-digit account number) used to identify an organization and receive the transaction.
    /// </summary>
    public required string BusinessShortCode { get; set; }

    /// <summary>
    /// This is the account number for which the customer is making the payment. This is only applicable to Customer PayBill Transactions.
    /// </summary>
    public string BillRefNumber { get; set; } = string.Empty;

    /// <summary>
    /// The invoice number (if any).
    /// </summary>
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// The current utility account balance of the payment-receiving organization shortcode. 
    /// For validation requests, this field is usually blank whereas, for the confirmation message, the value represents the new balance after the payment has been received.
    /// </summary>
    public decimal OrgAccountBalance { get; set; }

    /// <summary>
    /// This is a transaction ID that the partner can use to identify the transaction.
    /// When a validation request is sent, the partner can respond with ThirdPartyTransID and this will be sent back with the Confirmation notification.
    /// </summary>
    public string ThirdPartyTransID { get; set; } = string.Empty;

    /// <summary>
    /// This is a masked mobile number of the customer making the payment.
    /// </summary>
    public required string MSISDN { get; set; }

    /// <summary>
    /// The customer's first name is as per the M-Pesa register. This parameter can be empty.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// The customer's middle name is as per the M-Pesa register. This parameter can be empty.
    /// </summary>
    public string MiddleName { get; set; } = string.Empty;

    /// <summary>
    /// The customer's last name is as per the M-Pesa register. This parameter can be empty.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}

public enum RequestType
{
    Validation,
    Confirmation
}

public enum C2BTransactionType
{
    PayBill,
    BuyGoods
}

