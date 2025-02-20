using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et.Responses;

public class BaseResponse
{
    /// <summary>
    /// Gets or sets the response code.
    /// </summary>
    /// <value>The response code.</value>    
    public string? ResponseCode { get; set; }
    /// <summary>
	/// Response Description message
	/// </summary>
	/// <value>The response description.</value>
    public string? ResponseDescription { get; set; }  
}
