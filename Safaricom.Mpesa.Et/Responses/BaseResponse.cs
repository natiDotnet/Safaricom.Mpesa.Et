using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et.Responses;

public class BaseResponse
{
    public string? ResponseCode { get; set; }
    public string? ResponseDescription { get; set; }  
}
