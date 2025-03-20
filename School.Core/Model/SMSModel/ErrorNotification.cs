namespace School.Core.Model.SMSModel;

public class ErrorNotification : BaseSmsResponse
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public DateTime TimeStamp { get; set; }
}