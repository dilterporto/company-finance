using Company.Finance.Apis.Statements.Messages;
using FluentValidation;

namespace Company.Finance.Apis.Statements.Validators
{
    public class StatementsIngestionRequestValidator : AbstractValidator<StatementsIngestionRequest>
    {
        public StatementsIngestionRequestValidator()
        {
            RuleFor(x => x.StatementFiles)
                .NotNull()
                .WithMessage("files list are empty");
            RuleForEach(x => x.StatementFiles)
                .NotNull()
                .SetValidator(new FormFileValidator());
        }
    }
}