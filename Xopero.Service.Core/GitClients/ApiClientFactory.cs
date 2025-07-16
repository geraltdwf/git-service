using Microsoft.Extensions.DependencyInjection;
using Xopero.Service.Core.GitClients.GitHub;
using Xopero.Service.Core.GitClients.GitLab;
using Xopero.Service.Core.Interfaces;

namespace Xopero.Service.Core.GitClients;

public class ApiClientFactory : IApiClientFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ApiClientFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IIssueService GetApiClient(string provider)
    {
      return provider.ToLower() switch
        {
            ApiProviders.GitHub => _serviceProvider.GetRequiredService<GitHubClient>(),
            ApiProviders.GitLab => _serviceProvider.GetRequiredService<GitLabClient>(),
            _ => throw new KeyNotFoundException($"Unknown provider: {provider}")
        };
    }
}