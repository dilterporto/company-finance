using System.IO;
using FluentValidation.Results;
using MediatR;

namespace Company.Finance.Application.Statements.Commands
{
    public class IngestStatementFileCommand : IRequest<(ValidationResult result, string fileName)>
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public int Length { get; set; }
        public Stream Stream { get; set; }
    }
}