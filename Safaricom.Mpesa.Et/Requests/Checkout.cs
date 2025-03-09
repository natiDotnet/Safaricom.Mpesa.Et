using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Safaricom.Mpesa.Et.Shared;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// STK Push data transfer object
/// </summary>
public class CheckoutOnline
{
    /// <summary>
    /// This is a global unique Identifier for any submitted payment request.
    /// </summary>
    public required string MerchantRequestID { get; set; }

    /// <summary>
    /// This is organizations shortcode (Paybill or Buygoods - A 5 to 6 digit account number) 
    /// used to identify an organization and receive the transaction.
    /// </summary>
    public required string BusinessShortCode { get; set; }

    /// <summary>
    /// This is the password used for encrypting the request sent: A base64 encoded string. 
    /// The base64 string is a combination of Shortcode+Passkey+Timestamp
    /// The Defualt value is set by a private method that creates the necessary base64 encoded string
    /// Don't set this property if you have set the passKey property.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// This is the Timestamp of the transaction, 
    /// normaly in the formart of YEAR+MONTH+DATE+HOUR+MINUTE+SECOND (YYYYMMDDHHMMSS) 
    /// Each part should be atleast two digits apart from the year which takes four digits.        
    /// </summary>
    [JsonIgnore]
    public TimeSpan Timestamp { get; set; } = DateTime.Now.TimeOfDay;

    [JsonInclude]
    [JsonPropertyName(nameof(Timestamp))]
    private string timestamp => Timestamp.ToString("yyyyMMddHHmmss");

    /// <summary>
    /// This is the transaction type that is used to identify the transaction when sending the request to M-Pesa. 
    /// The transaction type for M-Pesa Express is "CustomerPayBillOnline" 
    /// </summary>
    [JsonIgnore]
    public required UssdTransactionType TransactionType { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TransactionType))]
    private string transactionType => TransactionType.ToString();


    /// <summary>
    /// This is the Amount transacted, normally a numeric value. Money that customer pays to the Shorcode. 
    /// Only whole numbers are supported.
    /// </summary>
    //[JsonIgnore]
    public required int Amount { get; set; }

    //[JsonInclude]
    //[JsonPropertyName(nameof(Amount))]
    //private string amount => Amount.ToString();
    /// <summary>
    /// The phone number sending money. The parameter expected is a Valid Safaricom Mobile Number 
    /// that is M-Pesa registered in the format 2517XXXXXXXX
    /// </summary>
    public required string PartyA { get; set; }

    /// <summary>
    /// The organization receiving the funds. The parameter expected is a 5 to 6 digit.
    /// This can be the same as BusinessShortCode value.
    /// </summary>
    public required string PartyB { get; set; }

    /// <summary>
    /// The Mobile Number to receive the STK Pin Prompt. 
    /// This number can be the same as PartyA value.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// A CallBack URL is a valid secure URL that is used to receive notifications from M-Pesa API. 
    /// It is the endpoint to which the results will be sent by M-Pesa API.
    /// </summary>
    public required Uri CallBackURL { get; set; }

    /// <summary>
    /// Account Reference: This is an Alpha-Numeric parameter that is defined by your system as an Identifier 
    /// of the transaction for CustomerPayBillOnline transaction type. Along with the business name, 
    /// this value is also displayed to the customer in the STK PIN Prompt message. 
    /// Maximum of 12 characters.
    /// </summary>
    public required string AccountReference { get; set; }

    /// <summary>
    /// This is any additional information/comment that can be sent along with the request from your system. 
    /// Maximum of 13 Characters.
    /// </summary>
    public required string TransactionDesc { get; set; }

    /// <summary>
    /// This is a list of key value pairs that can be sent along with the request.
    /// </summary>
    public Reference[] ReferenceData { get; set; } = [];
}
public record Reference(string Key, string Value);
public enum UssdTransactionType
{
    CustomerPayBillOnline,
    CustomerBuyGoodsOnline
}