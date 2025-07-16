using System.Text.Json;
using FluentValidation;
using Xopero.Service.Core.Interfaces;
using Xopero.Service.Core.Models.Requests;
using Microsoft.Extensions.Logging;

namespace Xopero.Service.Core.Services
{
    public class GitService : IGitService
    {
        private readonly IApiClientFactory _apiClientFactory;
        private readonly Func<string, IGitApiValidators> _validatorFactory;
        private readonly ILogger<GitService> _logger;
        public GitService(IApiClientFactory apiClientFactory, Func<string, IGitApiValidators> validatorFactory, ILogger<GitService> logger)
        {
            _apiClientFactory = apiClientFactory;
            _validatorFactory = validatorFactory;
            _logger = logger;
        }

        public async Task<string> CloseIssue(string provider, CloseIssueRequest request)
        {
            var validator = _validatorFactory(provider).CloseIssueValidator;
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Invalid request: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }
            
            var apiClient = _apiClientFactory.GetApiClient(provider);
            return await apiClient.CloseIssuesAsync(request);
            
        }

        public async Task<string> CreateIssue(string provider, CreateIssueRequest request)
        {
            var validator = _validatorFactory(provider).CreateIssueValidator;
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Invalid request: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            var apiClient = _apiClientFactory.GetApiClient(provider);
            return await apiClient.CreateIssuesAsync(request);

        }

        public async Task<string> UpdateIssue(string provider, UpdateIssueRequest request)
        {

            var validator = _validatorFactory(provider).UpdateIssueValidator;
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Invalid request: {Errors}", validationResult.Errors);
                throw new ValidationException(validationResult.Errors);
            }

            var apiClient = _apiClientFactory.GetApiClient(provider);
            return await apiClient.UpdateIssuesAsync(request);
        }


    }
}