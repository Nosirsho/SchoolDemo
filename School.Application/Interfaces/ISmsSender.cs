using School.Core.Model.SmsSender;

namespace School.Application.Interfaces;

public interface ISmsSender
{
    Task<SmsResponse> SendSms(string url, string txnId, string phoneNumber, string message);
}