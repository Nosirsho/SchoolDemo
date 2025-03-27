namespace School.Core.Model.SmsSender;
using System.Text.Json.Serialization;

public class SmsResponse
{
    public bool IsSuccess { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("txn_id")]
    public string TxnId { get; set; } = string.Empty;
    [JsonPropertyName("msg_id")]
    public int MsgId { get; set; }
    [JsonPropertyName("smsc_msg_parts")]
    public int SmscMsgParts { get; set; }
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; } = string.Empty;
    [JsonPropertyName("errorCode")]
    public int ErrorCode { get; set; }
    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; } = string.Empty;
}