using FluentValidation;
using Xopero.Service.Core.Interfaces;
using Xopero.Service.Core.Models.Requests;

namespace Xopero.Service.Core.Validators;

public class GitHubValidators : IGitApiValidators
{
    public GitHubValidators()
    {
        CreateIssueValidator = new InternalCreateIssueGithubValidator();
        UpdateIssueValidator = new InternalUpdateIssueGithubValidator();
        CloseIssueValidator = new InternalCloseIssueValidator();
    }
    public IValidator<CreateIssueRequest> CreateIssueValidator {get;}
    public IValidator<UpdateIssueRequest> UpdateIssueValidator {get;}
    public IValidator<CloseIssueRequest> CloseIssueValidator {get;}

    private class InternalCreateIssueGithubValidator : AbstractValidator<CreateIssueRequest>{
        public InternalCreateIssueGithubValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required."); 
            RuleFor(r => r.Owner).NotEmpty().WithMessage("Owner is required.");
            RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
        }
    }

    private class InternalUpdateIssueGithubValidator : AbstractValidator<UpdateIssueRequest>{
        public InternalUpdateIssueGithubValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required."); 
            RuleFor(r => r.Owner).NotEmpty().WithMessage("Owner is required.");
            RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
            RuleFor(r => r.IssueNumber).NotEmpty().GreaterThan(1).WithMessage("Issue number is required to be a positive integer.");
        }
    }

    private class InternalCloseIssueValidator : AbstractValidator<CloseIssueRequest>
    {
        public InternalCloseIssueValidator()
        {
            RuleFor(r => r.Owner).NotEmpty().WithMessage("Owner is required.");
            RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
            RuleFor(r => r.IssueNumber).NotEmpty().GreaterThan(1).WithMessage("Issue number is required to be a positive integer.");
        }
    }
}


// public class CreateIssueGithubValidator : AbstractValidator<CreateIssueRequest>
// {
//     public CreateIssueGithubValidator()
//     {
//         RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
//         RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required."); 
//         RuleFor(r => r.Owner).NotEmpty().WithMessage("Owner is required.");
//         RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
//     }
// }
//
// public class UpdateIssueGithubValidator : AbstractValidator<UpdateIssueRequest>
// {
//     public UpdateIssueGithubValidator()
//     {
//         RuleFor(r => r.Title).NotEmpty().WithMessage("Title is required.");
//         RuleFor(r => r.Description).NotEmpty().WithMessage("Description is required."); 
//         RuleFor(r => r.Owner).NotEmpty().WithMessage("Owner is required.");
//         RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
//         RuleFor(r => r.IssueNumber).GreaterThan(1).WithMessage("Issue number is required to be a positive integer.");
//     }
// }
//
//
// public class CloseIssueGithubValidator : AbstractValidator<CloseIssueRequest>
// {
//     public CloseIssueGithubValidator()
//     {
//         RuleFor(r => r.Owner).NotEmpty().WithMessage("Owner is required.");
//         RuleFor(r => r.Repository).NotEmpty().WithMessage("Repository is required.");
//         RuleFor(r => r.IssueNumber).GreaterThan(1).WithMessage("Issue number is required to be a positive integer.");
//     }
// }
//
