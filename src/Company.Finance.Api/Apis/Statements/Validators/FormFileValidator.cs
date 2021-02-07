using System.IO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Company.Finance.Apis.Statements.Validators
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator()
        {
            RuleFor(x => x.FileName)
                .Must(Path.HasExtension)
                .WithMessage("file has no extesion");
            RuleFor(x => x.FileName)
                .Must(s => Path.GetExtension(s).Equals(".ofx"))
                .WithMessage("invalid file extension");
            RuleFor(x => x.ContentType)
                .Must(s => s.Equals("application/octet-stream"))
                .WithMessage("invalid file type");
        }
    }
}