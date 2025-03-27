namespace School.Persistence.Entities;

public class NotificationLogEntity
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    public string Request { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public string RequestType { get; set; } = string.Empty;
}