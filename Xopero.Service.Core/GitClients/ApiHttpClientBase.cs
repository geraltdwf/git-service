namespace Xopero.Service.Core.GitClients;
public abstract class ApiHttpClientBase 
{
    protected readonly HttpClient _http;
    protected ApiHttpClientBase(HttpClient http) => _http = http;
}
