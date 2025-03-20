using School.Core.Model.SMSModel;

namespace School.Application.Interfaces.Notification;

public interface INotification
{
    Task<BaseSmsResponse> Send(string url, string phoneNumber, string msg);
}