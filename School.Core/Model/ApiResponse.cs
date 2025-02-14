namespace School.Core.Model;

public class ApiResponse<T>
{
    public int State { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ApiResponse(T data, int state = 1, string message = null)
    {
        Data = data;
        State = state;
        Message = message;
    }

    public ApiResponse(int state, string message = null)
    {
        State = state;
        Message = message;
    }
}