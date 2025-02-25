using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Safaricom.Mpesa.Et.Shared;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// STK Push data transfer object
/// </summary>
public class CheckoutOnline : MpesaRequest
{
    #region Properties
    /// <summary>
    /// This is a global unique Identifier for any submitted payment request.
    /// </summary>
    public Guid? MerchantRequestID { get; set; }

    /// <summary>
    /// This is organizations shortcode (Paybill or Buygoods - A 5 to 6 digit account number) 
    /// used to identify an organization and receive the transaction.
    /// </summary>
    public string BusinessShortCode { get; set; }
    
    /// <summary>
    /// This is the password used for encrypting the request sent: A base64 encoded string. 
    /// The base64 string is a combination of Shortcode+Passkey+Timestamp
    /// The Defualt value is set by a private method that creates the necessary base64 encoded string
    /// Don't set this property if you have set the passKey property.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// This is the Timestamp of the transaction, 
    /// normaly in the formart of YEAR+MONTH+DATE+HOUR+MINUTE+SECOND (YYYYMMDDHHMMSS) 
    /// Each part should be atleast two digits apart from the year which takes four digits.        
    /// </summary>
    [JsonIgnore]
    public TimeSpan Timestamp { get; set; } = DateTime.Now.TimeOfDay;

    [JsonInclude]
    [JsonPropertyName(nameof(Timestamp))]
    private string _timestamp => Timestamp.ToString("yyyyMMddHHmmss");
    /// <summary>
    /// This is the transaction type that is used to identify the transaction when sending the request to M-Pesa. 
    /// The transaction type for M-Pesa Express is "CustomerPayBillOnline" 
    /// </summary>
#if DEBUG
    public string TransactionType { get; set; } = Shared.TransactionType.CustomerPayBillOnline;
#else
    public string TransactionType { get; set; }
#endif

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
    /// The phone number sending money. The parameter expected is a Valid Safaricom Mobile Number 
    /// that is M-Pesa registered in the format 2517XXXXXXXX
    /// </summary>
    public string PartyA { get; set; }

    /// <summary>
    /// The organization receiving the funds. The parameter expected is a 5 to 6 digit.
    /// This can be the same as BusinessShortCode value.
    /// </summary>
    public string PartyB { get; set; }

    /// <summary>
    /// The Mobile Number to receive the STK Pin Prompt. 
    /// This number can be the same as PartyA value.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// A CallBack URL is a valid secure URL that is used to receive notifications from M-Pesa API. 
    /// It is the endpoint to which the results will be sent by M-Pesa API.
    /// </summary>
    public string CallBackURL { get; set; }

    /// <summary>
    /// Account Reference: This is an Alpha-Numeric parameter that is defined by your system as an Identifier 
    /// of the transaction for CustomerPayBillOnline transaction type. Along with the business name, 
    /// this value is also displayed to the customer in the STK PIN Prompt message. 
    /// Maximum of 12 characters.
    /// </summary>
    public string AccountReference { get; set; }

    /// <summary>
    /// This is any additional information/comment that can be sent along with the request from your system. 
    /// Maximum of 13 Characters.
    /// </summary>
    public string TransactionDesc { get; set; }

    public Reference[] ReferenceData { get; set; }
    
    /// <summary>
    /// Lipa Na Mpesa Online PassKey
    /// Provide the Passkey only if you want MpesaLib to Encode the Password for you.
    /// </summary>
    public string Passkey { get; set; }

    
    #endregion
    #region PrivateMethods
    /// <summary>
    /// This method creates the necessary base64 encoded string that encrypts the request sent 
    /// </summary>
    private string EncodePassword(string shortCode, string passkey, string timestamp)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{shortCode}{passkey}{timestamp}"));
    }
    #endregion

    public class Reference
    {
        public required string Key { get; set; }
        public required string Value { get; set; } 
    }
}