using School.Application.Interfaces;
using School.Core.Model;
using School.Core.Model.SmsSender;
using School.Core.Stores;

namespace School.Application.Services;

public class SmsService
{
    private readonly ISmsSender _smsSender;
    private readonly INotificationLogStore _notificationLogStore;

    public SmsService(ISmsSender smsSender, INotificationLogStore notificationLogStore)
    {
        _smsSender = smsSender;
        _notificationLogStore = notificationLogStore;
    }

    public async Task<SmsResponse> SendSms(string number, string message)
    {
        var url = "sendsms_v1.php";
        var txnId = Guid.NewGuid().ToString();
        var response = await _smsSender.SendSms(url, txnId, number, message);
        var notificationLog = new NotificationLog()
        {
            Id = Guid.NewGuid(),
            RequestId = new Guid(txnId),
            RequestType = "SEND_SMS",
            Request = "Request",
            Response = response.ToString()
        };
        await _notificationLogStore.SaveLog(notificationLog);
        
        return response;
    }
}