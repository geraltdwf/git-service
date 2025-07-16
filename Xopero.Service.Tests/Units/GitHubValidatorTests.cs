using Xopero.Service.Core.Models.Requests;
using Xopero.Service.Core.Validators;

namespace Xopero.Service.Tests.Units;

public class GitHubValidatorTests
{
    [Theory]
    [InlineData("", "Description", "Owner", "Repo", "Title is required.")]
    [InlineData("Title", "", "Owner", "Repo", "Description is required.")]
    [InlineData("Title", "Description", "", "Repo", "Owner is required.")]
    [InlineData("Title", "Description", "Owner", "", "Repository is required.")]
    [InlineData("   ", "Description", "Owner", "Repo", "Title is required.")]
    public async Task GitHubValidator_ValidateCreateIssue_WithInvalidInput_ReturnsFailed(string title, string description, string owner, string repo, string errorType)
    {
        //Arrange
        var validator = new GitHubValidators();
        var issueRequest = new CreateIssueRequest()
        {
            Description = description,
            Title = title,
            Owner = owner,
            Repository = repo,
        };
        
        //Act
        var result = await validator.CreateIssueValidator.ValidateAsync(issueRequest);
        Assert.False(result.IsValid);
        Assert.Contains(errorType, result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Theory]
    [InlineData("Proper", "Description", "Owner", "Repo")]
    [InlineData("Now its good", "Description", "Owner", "Repo")]
    public async Task GitHubValidator_ValidateCreateIssue_WithValidInputs_ReturnsSuccess(string title, string description, string owner, string repo)
    {
        //Arrange
        var validator = new GitHubValidators();
        var issueRequest = new CreateIssueRequest()
        {
            Description = description,
            Title = title,
            Owner = owner,
            Repository = repo,
        };
        
        //Act
        var result = await validator.CreateIssueValidator.ValidateAsync(issueRequest);
        Assert.True(result.IsValid);
        Assert.False(result.Errors.Any());
    }
    
    [Theory]
    [InlineData("", "Description", "Owner", "Repo", 123, "Title is required.")]
    [InlineData("Title", "", "Owner", "Repo", 123, "Description is required.")]
    [InlineData("Title", "Description", "Test", "Repo", 0, "Issue number is required to be a positive integer.")]
    [InlineData("Title", "Description", "Owner", "", 11, "Repository is required.")]
    [InlineData("   ", "Description", "Owner", "Repo", 33,"Title is required.")]
    public async Task GitHubValidator_ValidateUpdateIssue_WithInvalidInput_ReturnsFailed(string title, string description, string owner, string repo, int issueNumber, string errorType)
    {
        //Arrange
        var validator = new GitHubValidators();
        var issueRequest = new UpdateIssueRequest()
        {
            Description = description,
            Title =title,
            Owner = owner,
            Repository = repo,
            IssueNumber = issueNumber
        };
        
        //Act
        var result = await validator.UpdateIssueValidator.ValidateAsync(issueRequest);
        Assert.False(result.IsValid);
        Assert.Contains(errorType, result.Errors.Select(x => x.ErrorMessage));
    }
    
    
    [Theory]
    [InlineData("Proper", "Description", "Owner", "Repo", 100)]
    [InlineData("Now its good", "Description", "Owner", "Repo",200)]
    public async Task GitHubValidator_ValidateUpdateIssue_WithValidInputs_ReturnsSuccess(string title, string description, string owner, string repo, int issueNumber)
    {
        //Arrange
        var validator = new GitHubValidators();
        var issueRequest = new UpdateIssueRequest()
        {
            Description = description,
            Title =title,
            Owner = owner,
            Repository = repo,
            IssueNumber = issueNumber
        };
        
        var result = await validator.UpdateIssueValidator.ValidateAsync(issueRequest);
        Assert.True(result.IsValid);
        Assert.False(result.Errors.Any());
    }
    
        
    [Theory]
    [InlineData("Owner", "", 123, "Repository is required.")]
    [InlineData("", "Repo", 123, "Owner is required.")]
    [InlineData( "Owner", "Repo", 0, "Issue number is required to be a positive integer.")]
    public async Task GitHubValidator_ValidateCloseIssue_WithInvalidInputs_ReturnsFail(string owner, string repo, int issueNumber, string errorType)
    {
        //Arrange
        var validator = new GitHubValidators();
        var issueRequest = new CloseIssueRequest()
        {
            Owner = owner,
            Repository = repo,
            IssueNumber = issueNumber
        };
        
        //Act
        var result = await validator.CloseIssueValidator.ValidateAsync(issueRequest);
        Assert.False(result.IsValid);
        Assert.Contains(errorType, result.Errors.Select(x => x.ErrorMessage));
    }
    
    [Theory]
    [InlineData("Owner", "Repo", 100)]
    [InlineData("Owner", "Repo",200)]
    public async Task GitHubValidator_ValidateCloseIssue_WithValidInputs_ReturnsFail(string owner, string repo, int issueNumber)
    {
        //Arrange
        var validator = new GitHubValidators();
        var issueRequest = new CloseIssueRequest()
        {
            Owner = owner,
            Repository = repo,
            IssueNumber = issueNumber
        };
        //Act
        var result = await validator.CloseIssueValidator.ValidateAsync(issueRequest);
        Assert.True(result.IsValid);
        Assert.False(result.Errors.Any());
    }
}