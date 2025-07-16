namespace Xopero.Service.Core.GitClients.GitLab;

public static class GitLabRoutes
{
    public static string CreateIssue(string projectId) => $"api/v4/projects/{projectId}/issues";
    public static string CloseOrUpdateIssue(string projectId, int issueIid) => $"api/v4/projects/{projectId}/issues/{issueIid}";
}

