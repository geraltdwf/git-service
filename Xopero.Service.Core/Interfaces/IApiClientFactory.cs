namespace Xopero.Service.Core.Interfaces;

public interface IApiClientFactory
{
    IIssueService GetApiClient(string provider);
}