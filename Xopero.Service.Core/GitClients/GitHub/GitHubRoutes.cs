namespace Xopero.Service.Core.GitClients.GitHub;

public static class GitHubRoutes
{
    public static string CloseOrUpdateIssue(string owner, string repo, int issueNumber) =>  $"/repos/{owner}/{repo}/issues/{issueNumber}";
    public static string CreateIssue(string owner, string repo) =>  $"/repos/{owner}/{repo}/issues";
}