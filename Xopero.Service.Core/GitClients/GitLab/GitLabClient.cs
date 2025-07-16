using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Xopero.Service.Core.Interfaces;
using Xopero.Service.Core.Models.Enums;
using Xopero.Service.Core.Models.Requests;

namespace Xopero.Service.Core.GitClients.GitLab;

public class GitLabClient : ApiHttpClientBase, IIssueService
{
    private readonly ILogger<GitLabClient> _logger;
    public GitLabClient(HttpClient http,  ILogger<GitLabClient>  logger) : base(http)
    {
        _logger = logger;
    }

    public  async Task<string> CreateIssuesAsync(CreateIssueRequest issueRequest)
    {
        var url = GitLabRoutes.CreateIssue(issueRequest.Repository);
        var body = new
        {
            title = issueRequest.Title,
            description = issueRequest.Description,
        };
        var result = await _http.PostAsync(url, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
        await HandleErrorResponse(result, ApiActions.Create, issueRequest.Repository);
        var response = await result.Content.ReadAsStringAsync();
        return response;
    }

    public async Task<string> CloseIssuesAsync(CloseIssueRequest issueRequest)
    {
        var url = GitLabRoutes.CloseOrUpdateIssue(issueRequest.Repository, issueRequest.IssueNumber);
        var body = new
        {
            state_event = "close",
        };
        var result = await _http.PutAsync(url, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
        await HandleErrorResponse(result, ApiActions.Close,  issueRequest.Repository);
        var response = await result.Content.ReadAsStringAsync();
        return response;
    }

    public async Task<string>UpdateIssuesAsync(UpdateIssueRequest issueRequest)
    {
        var url = GitLabRoutes.CloseOrUpdateIssue(issueRequest.Repository, issueRequest.IssueNumber);
        var body = new
        {   
            title = issueRequest.Title,
            description = issueRequest.Description
        };
        var result = await _http.PutAsync(url, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
        await HandleErrorResponse(result, ApiActions.Update, issueRequest.Repository);
        var response = await result.Content.ReadAsStringAsync();
        return response;
    }

    private async Task HandleErrorResponse(HttpResponseMessage result, ApiActions action, string repository)
    {
        if (!result.IsSuccessStatusCode)
        {
            var errorResponse = await result.Content.ReadAsStringAsync();
            _logger.LogError($"Error {action.ToString()} issue for [{repository}]: [{result.StatusCode}], Response: {errorResponse}");
            throw new HttpRequestException($"Error {action} issue: {result.StatusCode}");
        }
    }
}


