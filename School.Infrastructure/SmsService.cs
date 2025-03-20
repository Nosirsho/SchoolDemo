using System.Net.Http.Json;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Http;
using School.Application.Interfaces.Notification;
using School.Core.Model;
using School.Core.Model.SMSModel;
using School.Infrastructure.Model;

namespace School.Infrastructure;

public class SmsService : INotification
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseUrl = $"https://api.osonsms.com/";
    private readonly IDictionary<string, string> config = new Dictionary<string, string>()
    {
        { "dlm", ";" },
        { "t", "23" },
        { "login", "Nosirsho" },
        { "pass_hash", "9df91eec6a841d50f397728e907e0703" },
        { "sender", "OsonSMS" }
    };

    public SmsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResponse<SendSmsResponse>> SendSmsAsync(SendSmsRequest request)
    {
        string txn_id = "10000002";
        string phone_number = "992927400719";
        var str_hash = Sha256Hash(txn_id + config["dlm"] + config["login"] + config["dlm"] + config["sender"] + config["dlm"] + phone_number + config["dlm"] + config["pass_hash"]);
        var client = _httpClientFactory.CreateClient("SMS");
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        if (request != null)
        {
            queryString["from"] = config["sender"];
            queryString["login"] = config["login"];
            queryString["t"] = config["t"];
            queryString["phone_number"] = request.PhoneNumber;
            queryString["msg"] = request.Msg;
            queryString["str_hash"] = str_hash;
            queryString["txn_id"] = txn_id;
        }
        var fullRequestUri = "query_sms.php" + (queryString.Count > 0 ? "?" + queryString : "");
        
        var response = await client.GetFromJsonAsync<SendSmsResponse>(fullRequestUri);
        
        return new  ApiResponse<SendSmsResponse>(response);
    }
    
    
    public async Task<BaseSmsResponse> Send(string url, string phoneNumber, string msg)
    {
        string txn_id = "10000001";
        var str_hash = Sha256Hash(txn_id + config["dlm"] + config["login"] + config["dlm"] + config["sender"] + config["dlm"] + phoneNumber + config["dlm"] + config["pass_hash"]);
        var client = _httpClientFactory.CreateClient("SMS");
        var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["from"] = config["sender"];
            queryString["login"] = config["login"];
            queryString["t"] = config["t"];
            queryString["phone_number"] = phoneNumber;
            queryString["msg"] = msg;
            queryString["str_hash"] = str_hash;
            queryString["txn_id"] = txn_id;
        
        var fullRequestUri = url + (queryString.Count > 0 ? "?" + queryString : "");
        try
        {
            var response = await client.GetAsync(fullRequestUri);

            if (response.IsSuccessStatusCode)
            {
                var sendSmsResponse = await response.Content.ReadFromJsonAsync<SendSmsResponse>();
                if (sendSmsResponse != null)
                {
                    var result = new NotificationResponse()
                    {
                        Status = sendSmsResponse.Status,
                        TxnId = sendSmsResponse.TxnId,
                        MsgId = sendSmsResponse.MsgId,
                        SmscMsgParts = sendSmsResponse.SmscMsgParts,
                        TimeStamp = sendSmsResponse.TimeStamp,
                        Success = true
                    };
                    return result;
                } else {
                    return new BaseSmsResponse()
                    {
                        Success = true,
                        Message = "Failed to deserialize successful response." + response.Content
                    };
                }
            } else if (response.StatusCode == System.Net.HttpStatusCode.Conflict) {
                // Handle the 409 Conflict
                var errorJson = await response.Content.ReadAsStringAsync();
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<SendSmsResponse>(errorJson);
                    
                    if (errorResponse != null)
                    {
                        var result = new NotificationResponse()
                        {
                            Status = errorResponse.Status,
                            TxnId = errorResponse.TxnId,
                            MsgId = errorResponse.MsgId,
                            SmscMsgParts = errorResponse.SmscMsgParts,
                            TimeStamp = errorResponse.TimeStamp,
                            Success = true
                        };
                        return result;
                        // Console.WriteLine($"SMS Error: Code={errorResponse.Code}, Message=\"{errorResponse.Msg}\", Timestamp={errorResponse.TimeStamp}");
                        // throw new Exception(errorResponse.Msg);
                    }
                    else
                    {
                        Console.WriteLine("Failed to deserialize error response.");
                        throw new Exception("Failed to deserialize error response.");
                    }
                } catch (JsonException ex) {
                    throw new Exception("Error parsing JSON: " + errorJson);
                }
            } else  {
                // Handle other non-success status codes
                Console.WriteLine($"HTTP Error: {response.StatusCode}");
                throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
            }
        } catch (Exception e) {
            Console.WriteLine($"HTTP Error: {e.Message}");
            throw new HttpRequestException($"HTTP Error: {e.Message}");
        }
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