using Xopero.Service.Core.Models.Requests;

namespace Xopero.Service.Core.Interfaces;

public interface IIssueService
{
    Task<string>  CreateIssuesAsync(CreateIssueRequest issueRequest);
    Task<string>  CloseIssuesAsync(CloseIssueRequest closeIssueRequest);
    Task<string>  UpdateIssuesAsync(UpdateIssueRequest issueRequest);
    
    string ProviderKey { get; }
}

