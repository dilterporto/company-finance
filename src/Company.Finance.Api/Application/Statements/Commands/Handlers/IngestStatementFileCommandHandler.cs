using System.Threading;
using System.Threading.Tasks;
using Company.Finance.Application.Statements.Parsers;
using Company.Finance.Application.Transactions.Commands;
using FluentValidation.Results;
using MediatR;

namespace Company.Finance.Application.Statements.Commands.Handlers
{
    public class IngestStatementFileCommandHandler : 
        IRequestHandler<IngestStatementFileCommand, (ValidationResult result, string fileName)>
    {
        private readonly IStreamToCommandParser<CreateStatementCommand> _streamToCommandParser;
        private readonly IMediator _mediator;

        public IngestStatementFileCommandHandler(
            IStreamToCommandParser<CreateStatementCommand> streamToCommandParser,
            IMediator mediator)
        {
            _streamToCommandParser = streamToCommandParser;
            _mediator = mediator;
        }
        
        public async Task<(ValidationResult result, string fileName)> Handle(
            IngestStatementFileCommand ingestStatementFileCommand, 
            CancellationToken cancellationToken)
        {
            var (result, createStatementCommand) = 
                await _streamToCommandParser.Parse(ingestStatementFileCommand);

            if (!result.IsValid) return (result, ingestStatementFileCommand.FileName);
            
            await ExecuteCrateStatementCommand(createStatementCommand, cancellationToken);
            await ExecuteCreateManyTransctionsCommand(createStatementCommand, cancellationToken);

            return (result, ingestStatementFileCommand.FileName);
        }

        private async Task ExecuteCreateManyTransctionsCommand(CreateStatementCommand createStatementCommand,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(CreateManyTransactionsCommand.CreateFrom(createStatementCommand), 
                cancellationToken);
        }

        private async Task ExecuteCrateStatementCommand(CreateStatementCommand createStatementCommand,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(createStatementCommand, cancellationToken);
        }
    }
}