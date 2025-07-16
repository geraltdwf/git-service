using FluentValidation;
using Xopero.Service.Core.Models.Requests;

namespace Xopero.Service.Core.Interfaces
{
    public interface IGitApiValidators
    {
        IValidator<CreateIssueRequest> CreateIssueValidator { get; }
        IValidator<UpdateIssueRequest> UpdateIssueValidator { get; }
        IValidator<CloseIssueRequest> CloseIssueValidator { get; }
    }
}