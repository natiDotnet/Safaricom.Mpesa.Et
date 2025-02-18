using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et;

public class MpesaConfig
{
    public const string Key = "Mpesa";
    public required string ConsumerKey { get; set; }
    public required string ConsumerSecret { get; set; }
    public string BasicAuth => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ConsumerKey}:{ConsumerSecret}"));
}