using System.ComponentModel.DataAnnotations;

namespace Xopero.Service.Core.Models.Requests;

public class UpdateIssueRequest
{
    public string Owner { get; init; } = string.Empty;
    public string Repository { get; init; } = string.Empty;
    
    public int IssueNumber { get; set; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}