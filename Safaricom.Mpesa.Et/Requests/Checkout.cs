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
public class CheckoutOnline
{
    #region Properties

    /// <summary>
    /// This is a global unique Identifier for any submitted payment request.
    /// </summary>
    public Guid? MerchantRequestID { get; private set; }

    /// <summary>
    /// This is organizations shortcode (Paybill or Buygoods - A 5 to 6 digit account number) 
    /// used to identify an organization and receive the transaction.
    /// </summary>
    public string BusinessShortCode { get; private set; }
    
    /// <summary>
    /// This is the password used for encrypting the request sent: A base64 encoded string. 
    /// The base64 string is a combination of Shortcode+Passkey+Timestamp
    /// The Defualt value is set by a private method that creates the necessary base64 encoded string
    /// Don't set this property if you have set the passKey property.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// This is the Timestamp of the transaction, 
    /// normaly in the formart of YEAR+MONTH+DATE+HOUR+MINUTE+SECOND (YYYYMMDDHHMMSS) 
    /// Each part should be atleast two digits apart from the year which takes four digits.        
    /// </summary>
    [JsonIgnore]
    public TimeSpan Timestamp { get; private set; } = DateTime.Now.TimeOfDay;

    [JsonInclude]
    [JsonPropertyName(nameof(Timestamp))]
    private string _timestamp => Timestamp.ToString("yyyyMMddHHmmss");
    /// <summary>
    /// This is the transaction type that is used to identify the transaction when sending the request to M-Pesa. 
    /// The transaction type for M-Pesa Express is "CustomerPayBillOnline" 
    /// </summary>
#if DEBUG
    public string TransactionType { get; private set; } = Shared.TransactionType.CustomerPayBillOnline;
#else
        public string TransactionType { get; private set; }
#endif

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
    /// The phone number sending money. The parameter expected is a Valid Safaricom Mobile Number 
    /// that is M-Pesa registered in the format 2517XXXXXXXX
    /// </summary>
    public string PartyA { get; private set; }

    /// <summary>
    /// The organization receiving the funds. The parameter expected is a 5 to 6 digit.
    /// This can be the same as BusinessShortCode value.
    /// </summary>
    public string PartyB { get; private set; }

    /// <summary>
    /// The Mobile Number to receive the STK Pin Prompt. 
    /// This number can be the same as PartyA value.
    /// </summary>
    public string PhoneNumber { get; private set; }

    /// <summary>
    /// A CallBack URL is a valid secure URL that is used to receive notifications from M-Pesa API. 
    /// It is the endpoint to which the results will be sent by M-Pesa API.
    /// </summary>
    public string CallBackURL { get; private set; }

    /// <summary>
    /// Account Reference: This is an Alpha-Numeric parameter that is defined by your system as an Identifier 
    /// of the transaction for CustomerPayBillOnline transaction type. Along with the business name, 
    /// this value is also displayed to the customer in the STK PIN Prompt message. 
    /// Maximum of 12 characters.
    /// </summary>
    public string AccountReference { get; private set; }

    /// <summary>
    /// This is any additional information/comment that can be sent along with the request from your system. 
    /// Maximum of 13 Characters.
    /// </summary>
    public string TransactionDesc { get; private set; }

    public Reference[] ReferenceData { get; set; }
    
    /// <summary>
    /// Lipa Na Mpesa Online PassKey
    /// Provide the Passkey only if you want MpesaLib to Encode the Password for you.
    /// </summary>
    public string Passkey { get; private set; }

    
    #endregion

    #region Constructor
    /// <summary>
    /// Mpesa Lipa Na Mpesa STK Push data transfer object
    /// </summary>
    /// <param name="businessShortCode">
    /// This is organizations shortcode (Paybill or Buygoods - A 5 to 6 digit account number) 
    /// used to identify an organization and receive the transaction.
    /// </param>
    /// <param name="timeStamp">
    /// This is the Timestamp of the transaction, 
    /// normaly in the formart of YEAR+MONTH+DATE+HOUR+MINUTE+SECOND (YYYYMMDDHHMMSS) 
    /// Each part should be atleast two digits apart from the year which takes four digits.    
    /// </param>
    /// <param name="transactionType">
    /// This is the transaction type that is used to identify the transaction when sending the request to M-Pesa. 
    /// The transaction type for M-Pesa Express is "CustomerPayBillOnline" 
    /// </param>
    /// <param name="amount">
    ///  This is the Amount transacted, normally a numeric value. Money that customer pays to the Shorcode. 
    /// Only whole numbers are supported.
    /// </param>
    /// <param name="partyA">
    ///  The phone number sending money. The parameter expected is a Valid Safaricom Mobile Number 
    /// that is M-Pesa registered in the format 2547XXXXXXXX
    /// </param>
    /// <param name="partyB">
    /// The organization receiving the funds. The parameter expected is a 5 to 6 digit.
    /// This can be the same as BusinessShortCode value.
    /// </param>
    /// <param name="phoneNumber"> 
    /// The Mobile Number to receive the STK Pin Prompt. 
    /// This number can be the same as PartyA value.
    /// </param>
    /// <param name="callBackUrl">
    /// A CallBack URL is a valid secure URL that is used to receive notifications from M-Pesa API. 
    /// It is the endpoint to which the results will be sent by M-Pesa API.
    /// </param>
    /// <param name="accountReference">
    /// Account Reference: This is an Alpha-Numeric parameter that is defined by your system as an Identifier 
    /// of the transaction for CustomerPayBillOnline transaction type. Along with the business name, 
    /// this value is also displayed to the customer in the STK PIN Prompt message. 
    /// Maximum of 12 characters.
    /// </param>
    /// <param name="transactionDescription">
    /// This is any additional information/comment that can be sent along with the request from your system. 
    /// Maximum of 13 Characters.
    /// </param>
    /// <param name="passkey">
    /// Lipa Na Mpesa Online PassKey
    /// Provide the Passkey only if you want MpesaLib to Encode the Password for you.
    /// </param>
    public CheckoutOnline(string businessShortCode, TimeSpan timeStamp, string transactionType, int amount,
        string partyA, string partyB, string phoneNumber, string callBackUrl, string accountReference,
        string transactionDescription, Reference[] referenceData, string passkey)
    {
        var formattedTimestamp = timeStamp.ToString("yyyyMMddHHmmss");

        BusinessShortCode = businessShortCode;
        Timestamp = timeStamp;
        TransactionType = transactionType;
        Amount = amount;
        PartyA = partyA;
        PartyB = partyB;
        PhoneNumber = phoneNumber;
        CallBackURL = callBackUrl;
        AccountReference = accountReference;
        TransactionDesc = transactionDescription;
        ReferenceData = referenceData;
        Passkey = passkey;
        Password = EncodePassword(BusinessShortCode, passkey, formattedTimestamp);
    }

    /// <summary>
    /// Mpesa Lipa Na Mpesa STK Push data transfer object
    /// </summary>
    /// <param name="merchantRequestID">
    /// This is a global unique Identifier for any submitted payment request.
    /// </param>
    /// <param name="businessShortCode">
    /// This is organizations shortcode (Paybill or Buygoods - A 5 to 6 digit account number) 
    /// used to identify an organization and receive the transaction.
    /// </param>
    /// <param name="timeStamp">
    /// This is the Timestamp of the transaction, 
    /// normaly in the formart of YEAR+MONTH+DATE+HOUR+MINUTE+SECOND (YYYYMMDDHHMMSS) 
    /// Each part should be atleast two digits apart from the year which takes four digits.    
    /// </param>
    /// <param name="transactionType">
    /// This is the transaction type that is used to identify the transaction when sending the request to M-Pesa. 
    /// The transaction type for M-Pesa Express is "CustomerPayBillOnline" 
    /// </param>
    /// <param name="amount">
    ///  This is the Amount transacted, normally a numeric value. Money that customer pays to the Shorcode. 
    /// Only whole numbers are supported.
    /// </param>
    /// <param name="partyA">
    ///  The phone number sending money. The parameter expected is a Valid Safaricom Mobile Number 
    /// that is M-Pesa registered in the format 2547XXXXXXXX
    /// </param>
    /// <param name="partyB">
    /// The organization receiving the funds. The parameter expected is a 5 to 6 digit.
    /// This can be the same as BusinessShortCode value.
    /// </param>
    /// <param name="phoneNumber"> 
    /// The Mobile Number to receive the STK Pin Prompt. 
    /// This number can be the same as PartyA value.
    /// </param>
    /// <param name="callBackUrl">
    /// A CallBack URL is a valid secure URL that is used to receive notifications from M-Pesa API. 
    /// It is the endpoint to which the results will be sent by M-Pesa API.
    /// </param>
    /// <param name="accountReference">
    /// Account Reference: This is an Alpha-Numeric parameter that is defined by your system as an Identifier 
    /// of the transaction for CustomerPayBillOnline transaction type. Along with the business name, 
    /// this value is also displayed to the customer in the STK PIN Prompt message. 
    /// Maximum of 12 characters.
    /// </param>
    /// <param name="transactionDescription">
    /// This is any additional information/comment that can be sent along with the request from your system. 
    /// Maximum of 13 Characters.
    /// </param>
    /// <param name="passkey">
    /// Lipa Na Mpesa Online PassKey
    /// Provide the Passkey only if you want MpesaLib to Encode the Password for you.
    /// </param>
    public CheckoutOnline(Guid merchantRequestID,string businessShortCode, TimeSpan timeStamp, string transactionType, int amount,
        string partyA, string partyB, string phoneNumber, string callBackUrl, string accountReference,
        string transactionDescription, Reference[] referenceData, string passkey)
        : this(businessShortCode, timeStamp, transactionType, amount, partyA, partyB, phoneNumber, callBackUrl, accountReference, transactionDescription, referenceData, passkey)
    {
        MerchantRequestID = merchantRequestID;
    }
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