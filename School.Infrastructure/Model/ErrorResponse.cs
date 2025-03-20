namespace School.Infrastructure.Model;

public class ErrorResponse
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public DateTime TimeStamp { get; set; }
}