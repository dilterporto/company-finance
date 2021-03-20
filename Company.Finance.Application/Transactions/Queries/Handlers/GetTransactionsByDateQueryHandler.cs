using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Company.Finance.Application.Transactions.Queries.Responses;
using Company.Finance.Domain.Entities;
using Company.Finance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Company.Finance.Application.Transactions.Queries.Handlers
{
    public class GetTransactionsByDateQueryHandler :
        IRequestHandler<GetTransactionsByDateQuery, IEnumerable<TransactionResponse>>
    {
        private readonly FinanceDbContext _context;
        private readonly IMapper _mapper;

        public GetTransactionsByDateQueryHandler(FinanceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<TransactionResponse>> Handle(GetTransactionsByDateQuery query, 
            CancellationToken cancellationToken)
        {
            return FilterTransactions(query);
        }

        private IEnumerable<TransactionResponse> FilterTransactions(GetTransactionsByDateQuery query)
        {
            return _context.Set<Transaction>()
                .Where(x => query.Filter.From.HasValue && x.Date.Value >= query.Filter.From.Value.Date)
                .Where(x => query.Filter.To.HasValue && x.Date.Value <= query.Filter.To.Value.Date)
                .AsNoTracking()
                .AsEnumerable()
                .Select(_mapper.Map<TransactionResponse>);
        }
    }
}