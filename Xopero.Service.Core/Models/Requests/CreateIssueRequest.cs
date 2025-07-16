namespace Xopero.Service.Core.Models.Requests;

public class CreateIssueRequest
{
    public string Owner { get; init; } = string.Empty;
    public string Repository { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}