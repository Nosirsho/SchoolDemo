using School.Application.Interfaces.Notification;
using School.Core.Model;

namespace School.Application.Services;

public class NotificationService
{
    private readonly INotification _notification;

    public NotificationService(INotification notification)
    {
        _notification = notification;
    }

    public async Task<NotificationResponse> NotifyAsync(string msg)
    {
        var url = "sendsms_v1.php";
        var number = "992927400719";
        return await _notification.Send(url, number, msg);
    }
}