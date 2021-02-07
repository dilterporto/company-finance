using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Company.Finance.Models.Entities;
using Company.Finance.Persistence;
using MediatR;

namespace Company.Finance.Application.Statements.Commands.Handlers
{
    public class CreateStatementCommandHandler : IRequestHandler<CreateStatementCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStatementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(CreateStatementCommand createStatementCommand, 
            CancellationToken cancellationToken)
        {
            var statement = _mapper.Map<Statement>(createStatementCommand);
            await _unitOfWork.Add(statement);
            
            return Unit.Value;
        }
    }
}