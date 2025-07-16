using System.Text.Json;
using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Xopero.Service.Core.Interfaces;
using Xopero.Service.Core.Models.Errors;
using Xopero.Service.Core.Models.Requests;
using Xopero.Service.Core.Validators;

namespace Xopero.Service.Rest.Controllers;


[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class GitController : ControllerBase
{
    private readonly ILogger<GitController> _logger;
    private readonly IGitService _gitService;
    public GitController( IGitService gitService, ILogger<GitController> logger)
    {
        _gitService = gitService;
        _logger = logger;
    }

    /// <summary>
    /// Create an issue for the specified provider (GitHub or GitLab)
    /// </summary>
    /// <param name="provider">Name of the provider (github or gitlab)</param>
    /// <param name="request">Request body which contains issue create details</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ValidationException"></exception>
    [HttpPost("/git/create/{provider}")]
    public async Task<IActionResult> CreateIssue(string provider, [FromBody] CreateIssueRequest request)
    {
        try
        {
            var result = await _gitService.CreateIssue(provider, request);
            return Ok(JsonBeautify(result));
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Validation error occured");
            return BadRequest(new ErrorResponse()
            {
                Message = ex.Message,
                ErrorCode = ErrorCodes.ValidationError
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occured");
            return BadRequest(new ErrorResponse()
            {
                Message = ex.Message,
                ErrorCode = ErrorCodes.UnexpectedError
            });
        }

    }
    
    /// <summary>
    /// Update an issue on the specified provider (GitHub or GitLab)
    /// </summary>
    /// <param name="provider">Name of the provider (github or gitlab)</param>
    /// <param name="request">Request body which contains issue update details</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ValidationException"></exception>
    [HttpPut("/git/update/{provider}")]
    public async Task<IActionResult> UpdateIssue(string provider, [FromBody] UpdateIssueRequest request)
    {
        try
        {
            var result = await _gitService.UpdateIssue(provider, request);
            return Ok(JsonBeautify(result));
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Validation error occured");
            return BadRequest(new ErrorResponse()
            {
                Message = ex.Message,
                ErrorCode = ErrorCodes.ValidationError
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occured");
            return BadRequest(new ErrorResponse()
            {
                Message = ex.Message,
                ErrorCode = ErrorCodes.UnexpectedError
            });
        }
    }
    
    
    
    /// <summary>
    /// Create an issue for the specified provider (GitHub or GitLab)
    /// </summary>
    /// <param name="provider">Name of the provider (github or gitlab)</param>
    /// <param name="request">Request body which contains issue close details</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ValidationException"></exception>
    [HttpPut("/git/close/{provider}")]
    public async Task<IActionResult> CloseIssue(string provider, [FromBody] CloseIssueRequest request)
    {
        try
        {
            var result = await _gitService.CloseIssue(provider, request);
            return Ok(JsonBeautify(result));
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Validation error occured");
            return BadRequest(new ErrorResponse()
            {
                Message = ex.Message,
                ErrorCode = ErrorCodes.ValidationError
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occured");
            return BadRequest(new ErrorResponse()
            {
                Message = ex.Message,
                ErrorCode = ErrorCodes.UnexpectedError
            });
        }

    }

    private string JsonBeautify(string response) => JsonSerializer.Serialize(JsonDocument.Parse(response),
            new JsonSerializerOptions() { WriteIndented = true });
}