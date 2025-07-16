using Xopero.Service.Core.Models.Requests;

namespace Xopero.Service.Core.Interfaces
{
   public interface IGitService
{
    Task<string> CreateIssue(string provider, CreateIssueRequest request);
    Task<string> UpdateIssue(string provider, UpdateIssueRequest request);
    Task<string> CloseIssue(string provider, CloseIssueRequest request);
}
}