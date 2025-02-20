using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Safaricom.Mpesa.Et.Requests;
using Safaricom.Mpesa.Et.Responses;

namespace Safaricom.Mpesa.Et;

public interface IMpesaClient
{
    Task<AuthResponse?> AuthorizeAsync(CancellationToken cancellationToken = default);
    Task<MpesaResponse?> TransactionStatusAsync(TransactionStatus request, CancellationToken cancellationToken = default);
    Task<MpesaResponse?> AccountBalanceAsymc(AccountBalance request, CancellationToken cancellationToken = default);
}
