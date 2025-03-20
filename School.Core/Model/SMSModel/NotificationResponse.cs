namespace School.Core.Model.SMSModel;

public class NotificationResponse : BaseSmsResponse
{
    public string Status { get; set; } = string.Empty;
    public string TxnId { get; set; } = string.Empty;
    public string MsgId { get; set; } = string.Empty;
    public string SmscMsgParts { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; }
}