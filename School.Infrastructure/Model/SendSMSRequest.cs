namespace School.Infrastructure.Model;

public class SendSmsRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Msg { get; set; } = string.Empty;
    
}