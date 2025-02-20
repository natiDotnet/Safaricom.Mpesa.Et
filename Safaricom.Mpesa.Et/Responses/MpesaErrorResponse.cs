using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et.Responses;

public class MpesaErrorResponse
{
    public string? RequestId { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}
