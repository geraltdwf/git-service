using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Xopero.Service.Core.Interfaces;
using Xopero.Service.Core.Models.Enums;
using Xopero.Service.Core.Models.Requests;

namespace Xopero.Service.Core.GitClients.GitHub;

public class GitHubClient : ApiHttpClientBase, IIssueService
{
    private readonly ILogger<GitHubClient> _logger;
    public GitHubClient(HttpClient http, ILogger<GitHubClient>  logger) : base(http)
    {
        _logger = logger;
    }

    public async Task<string> CreateIssuesAsync(CreateIssueRequest issueRequest)
    {
        var url = GitHubRoutes.CreateIssue(issueRequest.Owner, issueRequest.Repository);
        var body = new
        {
            title = issueRequest.Title,
            body = issueRequest.Description
        };
        var result = await _http.PostAsync(url, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
        await HandleErrorResponse(result, ApiActions.Create, issueRequest.Owner,issueRequest.Repository);

        var response = await result.Content.ReadAsStringAsync();
        return response;
    }

    public async Task<string> CloseIssuesAsync(CloseIssueRequest issueRequest )
    {
        var url = GitHubRoutes.CloseOrUpdateIssue(issueRequest.Owner, issueRequest.Repository, issueRequest.IssueNumber);
        var body = new
        {
            state = "Closed",
        };
        var result = await _http.PatchAsync(url, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
        await HandleErrorResponse(result, ApiActions.Close, issueRequest.Owner,issueRequest.Repository);


        var response = await result.Content.ReadAsStringAsync();
        return response;
    }

    public async Task<string>UpdateIssuesAsync(UpdateIssueRequest issueRequest)
    {
        
        var url = GitHubRoutes.CloseOrUpdateIssue(issueRequest.Owner, issueRequest.Repository, issueRequest.IssueNumber);
        var body = new
        {
            title = issueRequest.Title,
            body = issueRequest.Description,
        };
        var result = await _http.PatchAsync(url, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
        await HandleErrorResponse(result, ApiActions.Update, issueRequest.Owner,issueRequest.Repository);
        
        var response = await result.Content.ReadAsStringAsync();
        return response;
    }
    
    
    private async Task HandleErrorResponse(HttpResponseMessage result, ApiActions action, string owner, string repository)
    {
        if (!result.IsSuccessStatusCode)
        {
            var errorResponse = await result.Content.ReadAsStringAsync();
            _logger.LogError($"Error {action.ToString()} issue for [{owner}|{repository}]: [{result.StatusCode}], Response: {errorResponse}");
            throw new HttpRequestException($"Error {action} issue: {result.StatusCode}");
        }
    }

}





