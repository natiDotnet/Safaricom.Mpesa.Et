using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Safaricom.Mpesa.Et.Shared;

public static class Helper
{
    public static JsonSerializerOptions SnakeCase => new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };

    public static JsonSerializerOptions PascalCase => new JsonSerializerOptions
    {
        PropertyNamingPolicy = null,
        IncludeFields = true,
        WriteIndented = true
    };
    // public static string EncodePassword(string shortCode, string passkey, string timestamp)
    // {
    //     return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{shortCode}{passkey}{timestamp}"));
    // }
}
