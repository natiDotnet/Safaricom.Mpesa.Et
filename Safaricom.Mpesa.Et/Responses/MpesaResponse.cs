
namespace Safaricom.Mpesa.Et.Responses;

public class MpesaResponse : BaseResponse
{
    /// <summary>
	/// The unique request ID for tracking a transaction
	/// </summary>
	/// <value>The originator coversation identifier.</value>
    public string? OriginatorConversationID { get; set; }
    /// <summary>
	/// The unique request ID returned by mpesa for each request made
	/// </summary>
	/// <value>The conversation identifier.</value>
    public string? ConversationID { get; set; }   
}