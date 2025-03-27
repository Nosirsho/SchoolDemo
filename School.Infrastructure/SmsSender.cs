using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Options;
using School.Application.Interfaces;
using School.Core.Model.SmsSender;

namespace School.Infrastructure;

public class SmsSender : ISmsSender
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<NotificationSettings> _notificationSettings;

    public SmsSender(IHttpClientFactory httpClientFactory, IOptions<NotificationSettings> notificationSettings)
    {
        _httpClientFactory = httpClientFactory;
        _notificationSettings = notificationSettings;
    }
    public async Task<SmsResponse> SendSms(string url, string txnId, string phoneNumber, string message)
    {
        var dlm = _notificationSettings.Value.Delimetr;
        var login = _notificationSettings.Value.Login;
        var sender = _notificationSettings.Value.Sender;
        var pass_hash = _notificationSettings.Value.PassHash;
        
        var str_hash = Sha256Hash(txnId + dlm + login + dlm + sender + dlm + phoneNumber + dlm + pass_hash);
        var client = _httpClientFactory.CreateClient("SMS");
        var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["from"] = sender;
            queryString["login"] = login;
            queryString["t"] = _notificationSettings.Value.Type;
            queryString["phone_number"] = phoneNumber;
            queryString["msg"] = message;
            queryString["str_hash"] = str_hash;
            queryString["txn_id"] = txnId;
        
        var fullRequestUri = url + (queryString.Count > 0 ? "?" + queryString : "");
        try
        {
            var response = await client.GetAsync(fullRequestUri);

            if (response.IsSuccessStatusCode)
            {
                var sendSmsResponse = await response.Content.ReadFromJsonAsync<SmsResponse>();
                if (sendSmsResponse != null)
                {
                    var result = new SmsResponse()
                    {
                        IsSuccess = true,
                        Status = sendSmsResponse.Status,
                        TxnId = sendSmsResponse.TxnId,
                        MsgId = sendSmsResponse.MsgId,
                        SmscMsgParts = sendSmsResponse.SmscMsgParts,
                        Timestamp = sendSmsResponse.Timestamp
                    };
                    return result;
                }

                if(response.StatusCode == (System.Net.HttpStatusCode)402) {
                    var errorJson = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
                    
                        if (errorResponse != null)
                        {
                            return new SmsResponse
                            {
                                IsSuccess = false,
                                ErrorCode = errorResponse.Error.Code,
                                ErrorMessage = errorResponse.Error.Msg,
                                Timestamp = errorResponse.Error.Timestamp
                            };
                        }
                        
                        return new SmsResponse
                        {
                            IsSuccess = false,
                            ErrorMessage = $"Failed to deserialize error response"
                        };
                    } catch (JsonException ex) {
                        return new SmsResponse
                        {
                            IsSuccess = false,
                            ErrorMessage = $"\"Error parsing JSON: \" + {errorJson} Exception: {ex.Message}"
                        };
                    }
                }
            } else if (response.StatusCode == System.Net.HttpStatusCode.Conflict) {
                var errorJson = await response.Content.ReadAsStringAsync();
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<SmsResponse>(errorJson);
                    
                    if (errorResponse != null)
                    {
                        var result = new SmsResponse()
                        {
                            IsSuccess = true,
                            Status = errorResponse.Status,
                            TxnId = errorResponse.TxnId,
                            MsgId = errorResponse.MsgId,
                            SmscMsgParts = errorResponse.SmscMsgParts,
                            Timestamp = errorResponse.Timestamp
                        };
                        return result;
                    }

                    return new SmsResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = $"Error: {response.StatusCode} - Failed to deserialize error response."
                    };
                } catch (JsonException ex) {
                    return new SmsResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = $"Error: {response.StatusCode} - Error parsing JSON: \" + { errorJson } Exception: {ex.Message}"
                    };
                }
            } else  {
                return new SmsResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"HTTP Error: {response.StatusCode}"
                };
            }
        } catch (Exception e) {
            return new SmsResponse
            {
                IsSuccess = false,
                ErrorMessage = $"HTTP Error: {e.Message}"
            };
        }

        return new SmsResponse
        {
            IsSuccess = false,
            ErrorMessage = $"HTTP "
        };
    }
    
    private string Sha256Hash(string value)
    {
        var Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }
}