using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Safaricom.Mpesa.Et.Shared;

namespace Safaricom.Mpesa.Et.Requests;

/// <summary>
/// C2B Simulate data transfer object
/// </summary>
public class C2BSimulate
{
    /// <summary>
    /// This is the Short Code receiving the amount being transacted.
    /// </summary>
    public string ShortCode { get; set; }

    /// <summary>
    /// This is a unique identifier of the transaction type: There are two types of these Identifiers:
    /// CustomerPayBillOnline: This is used for Pay Bills shortcodes.
    /// CustomerBuyGoodsOnline: This is used for Buy Goods shortcodes.
    /// Buy Default this property is set to CustomerPayBillOnline
    /// </summary>
    public string CommandID { get; set; } = TransactionType.CustomerPayBillOnline;

    /// <summary>
    /// This is the amount being transacted. The parameter expected is a numeric value.
    /// </summary>
    [JsonIgnore]
    public int Amount { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Amount))]
    private string _amount => Amount.ToString();
    
    /// <summary>
    /// This is the phone number initiating the C2B transaction.(format: 2547XXXXXXXX)
    /// </summary>
    public string Msisdn { get; set; }

    /// <summary>
    /// This is used on CustomerPayBillOnline option only. 
    /// This is where a customer is expected to enter a unique bill identifier, e.g an Account Number. 
    /// </summary>
    public string BillRefNumber { get; set; }
}