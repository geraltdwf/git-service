namespace Xopero.Service.Core.Models.Errors;

public class ErrorResponse
{
    public string Message {get;set;} = string.Empty;
    public int? ErrorCode {get;set;}
}