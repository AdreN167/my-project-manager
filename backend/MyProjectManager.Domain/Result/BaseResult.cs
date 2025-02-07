namespace MyProjectManager.Domain.Result;

public class BaseResult
{
    // сразу проверяем, есть ли у нас сообщение об ошибке, и записываем результат проверки
    public bool IsSuccess => ErrorMessage == null;
    public string ErrorMessage {  get; set; }
    public int? ErrorCode { get; set; }
}

public class BaseResult<T> : BaseResult
{
    public BaseResult() { }
    public BaseResult(string errorMessage, int errorCode, T date)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
        Data = date;
    }
    public T Data { get; set; }
}
