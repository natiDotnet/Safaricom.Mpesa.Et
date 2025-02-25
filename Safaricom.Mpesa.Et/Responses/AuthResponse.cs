using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et.Responses;

public class AuthResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
    public string ExpiresIn { get; set; } = string.Empty;
}

public class AuthErrorResponse
{
    public string? ResultCode { get; set; }
    public string? ResultDesc { get; set; }   
    
}
