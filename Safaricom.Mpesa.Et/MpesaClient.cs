using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et;

public class MpesaClient : IMpesaClient
{
    public MpesaClient(MpesaConfig config, HttpClient? client = null)
    {
        
    }
    public MpesaClient(string consumerKey, string consumerSecret)
       : this(new MpesaConfig { ConsumerKey = consumerKey, ConsumerSecret = consumerSecret })
    { }    
    
}
