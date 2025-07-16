using FluentValidation;
using Xopero.Service.Core.Interfaces;
using Xopero.Service.Core.Models.Requests;

namespace Xopero.Service.Core.Validators;


public class GitLabValidator : IGitApiValidators
{
    public GitLabValidator()
    {
        CreateIssueValidator = new InternalCreateIssueGitLabValidator();
        UpdateIssueValidator = new InternalUpdateIssueGitLabValidator();
        CloseIssueValidator = new InternalCloseIssueGitLabValidator();
    }
    public IValidator<CreateIssueRequest> CreateIssueValidator { get; }
    public IValidator<UpdateIssueRequest> UpdateIssueValidator { get; }
    public IValidator<CloseIssueRequest> CloseIssueValidator { get; }

    private class InternalCreateIssueGitLabValidator : AbstractValidator<CreateIssueRequest>
    {
        public InternalCreateIssueGitLabValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required."); 
            RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
        }
    }

    private class InternalUpdateIssueGitLabValidator : AbstractValidator<UpdateIssueRequest>
    {
        public InternalUpdateIssueGitLabValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required."); 
            RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
            RuleFor(r => r.IssueNumber).GreaterThan(1).WithMessage("Issue number is required to be a positive integer.");
        }
    }

    private class InternalCloseIssueGitLabValidator : AbstractValidator<CloseIssueRequest>
    {
        public InternalCloseIssueGitLabValidator()
        {
            RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
            RuleFor(r => r.IssueNumber).GreaterThan(1).WithMessage("Issue number is required to be a positive integer.");
        }
    }
}

// public class CreateIssueGitLabValidator : AbstractValidator<CreateIssueRequest>
// {
//     public CreateIssueGitLabValidator()
//     {
//         RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
//         RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required."); 
//         RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
//     }
// }
//
// public class UpdateIssueGitLabValidator : AbstractValidator<UpdateIssueRequest>
// {
//     public UpdateIssueGitLabValidator()
//     {
//         RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
//         RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required."); 
//         RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
//         RuleFor(r => r.IssueNumber).GreaterThan(1).WithMessage("Issue number is required to be a positive integer.");
//     }
// }
//
//
// public class CloseIssueGitLabValidator : AbstractValidator<CloseIssueRequest>
// {
//     public CloseIssueGitLabValidator()
//     {
//         RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
//         RuleFor(r => r.IssueNumber).GreaterThan(1).WithMessage("Issue number is required to be a positive integer.");
//     }
// }
