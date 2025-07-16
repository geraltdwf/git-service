namespace Xopero.Service.Core.Models.Requests;

public class CloseIssueRequest
{
    public string Owner { get; init; } = string.Empty;

    public string Repository { get; init; } = string.Empty;
    
    public int IssueNumber { get; set; }

    
}