using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et.Requests;

public class MpesaRequest
{
    [JsonInclude]
    protected string? Type = nameof(MpesaRequest);
}
