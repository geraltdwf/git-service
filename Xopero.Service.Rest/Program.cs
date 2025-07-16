using System.Net.Http.Headers;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using Polly;
using Xopero.Service.Core.Configuration;
using Xopero.Service.Core.GitClients;
using Xopero.Service.Core.GitClients.GitHub;
using Xopero.Service.Core.GitClients.GitLab;
using Xopero.Service.Core.Interfaces;
using Xopero.Service.Core.Services;
using Xopero.Service.Core.Validators;
using Xopero.Service.Rest.Filters;
using Xopero.Service.Rest.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddControllers();
builder.Services.AddProblemDetails().AddErrorObjects();
builder.Services.Configure<GitHubOptions>(builder.Configuration.GetSection("GithubApi"));
builder.Services.Configure<GitLabOptions>(builder.Configuration.GetSection("GitlabApi"));
builder.Services.AddHttpClient<GitHubClient>((sp, cp) =>
{
    var githubOptions = sp.GetRequiredService<IOptions<GitHubOptions>>().Value;
    cp.BaseAddress = new Uri(githubOptions.BaseUrl);
    cp.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
    cp.DefaultRequestHeaders.Add("X-GitHub-Api-Version", githubOptions.ApiVersion);
    cp.DefaultRequestHeaders.UserAgent.ParseAdd("TestService");
    cp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", githubOptions.Token);
}).AddResilienceHandler("default", cfg =>
{
    cfg.AddTimeout(TimeSpan.FromSeconds(5));
});

builder.Services.AddHttpClient<GitLabClient>((sp, cp) =>
{
    var gitLabOptions = sp.GetRequiredService<IOptions<GitLabOptions>>().Value;
    cp.BaseAddress = new Uri(gitLabOptions.BaseUrl);
    cp.DefaultRequestHeaders.Add("Accept", "application/json");
    cp.DefaultRequestHeaders.Add("PRIVATE-TOKEN", gitLabOptions.Token);
}).AddResilienceHandler("default", cfg =>
{
    cfg.AddTimeout(TimeSpan.FromSeconds(5));
});
builder.Services.AddSingleton<GitHubValidators>();
builder.Services.AddSingleton<GitLabValidator>();
builder.Services.AddSingleton<Func<string, IGitApiValidators>>(sp => provider =>
{
    return provider.ToLower() switch
    {
        "github" => sp.GetRequiredService<GitHubValidators>(),
        "gitlab" => sp.GetRequiredService<GitLabValidator>(),
        _ => throw new InvalidOperationException($"Validator for provider '{provider}' not found.")
    };
});

// ✅ CORRECT: Only register the factory, not multiple IIssueService implementations
builder.Services.AddScoped<IApiClientFactory, ApiClientFactory>();
builder.Services.AddScoped<IGitService, GitService>();
builder.Services.AddApiVersioning(apiOptions =>
{
    apiOptions.DefaultApiVersion = new ApiVersion(1, 0);
    apiOptions.AssumeDefaultVersionWhenUnspecified = false;
    apiOptions.ReportApiVersions = true; 
    apiOptions.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
}).AddMvc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    var apiVersion = builder.Configuration["ApiVersion"];
    if(string.IsNullOrEmpty(apiVersion))
        throw new InvalidOperationException("Configuration key 'ApiVersion' is not set. Please add it to your appsettings.json or environment variables.");
    opt.OperationFilter<AddApiVersionHeaderParameter>(apiVersion);
});

    
var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
