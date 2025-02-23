using Safaricom.Mpesa.Et.Requests;
using Safaricom.Mpesa.Et.Responses;

namespace Safaricom.Mpesa.Et;

public interface IMpesaClient
{
    Task<AuthResponse?> AuthorizeAsync(CancellationToken cancellationToken = default);
    Task<MpesaResponse?> TransactionStatusAsync(TransactionStatus request, CancellationToken cancellationToken = default);
    Task<MpesaResponse?> AccountBalanceAsync(AccountBalance request, CancellationToken cancellationToken = default);
    Task<MpesaResponse?> ReverseTransactionAsync(TransactionReversal request, CancellationToken cancellationToken = default);
    Task<MpesaResponse?> PayoutAsync(Payment request, CancellationToken cancellationToken = default);
    Task<MpesaResponse?> UssdPushAsync(CheckoutOnline request, CancellationToken cancellationToken = default);
    Task<MpesaResponse?> RegisterUrlAsync(string userName, C2BRegisterUrl request, CancellationToken cancellationToken = default);
}
