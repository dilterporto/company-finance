using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Company.Finance.Apis.Statements.Messages;
using Company.Finance.Application.Statements.Commands;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Company.Finance.Apis.Statements
{
    /// <summary>
    /// Statements controller
    /// </summary>
    [ApiController]
    [Route("statements")]
    public class StatementsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StatementsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Ingest statements via OFX files
        /// </summary>
        /// <param name="request">OFX list of files</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(object), 206)]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] StatementsIngestionRequest request)
        {
            return IngestionResult(
                await IngestStatementsAsync(request));
        }

        private IActionResult IngestionResult((ValidationResult result, string file)[] results)
        {
            var statusCode = (int)(results.Any(x => !x.result.IsValid)
                ? HttpStatusCode.PartialContent
                : HttpStatusCode.Accepted);
            
            return StatusCode(statusCode, results.Select(x => new
            {
                FileName = x.file,
                Ingested = x.result.IsValid,
                Errors = x.result.IsValid ? null : x.result.Errors
            }));
        }
        
        private async Task<(ValidationResult, string)[]> IngestStatementsAsync(StatementsIngestionRequest request)
        {
            var ingestStatementFileCommands = request
                .StatementFiles
                .Select(statementFile =>
                {
                    var ingestStatementFileCommand = _mapper.Map<IngestStatementFileCommand>(statementFile);
                    return SendCommandAsync<IngestStatementFileCommand, 
                        (ValidationResult result, string fileName)>(ingestStatementFileCommand);
                });

            var results = new List<(ValidationResult, string)>();
            foreach (var ingestStatementFileCommand in ingestStatementFileCommands)
            {
                var result = await ingestStatementFileCommand;
                results.Add(result);
            }
            return results.ToArray();
        }
        
        private async Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command) 
            where TCommand : IRequest<TResult>
        {
            return await _mediator.Send(command);
        }
    }
}