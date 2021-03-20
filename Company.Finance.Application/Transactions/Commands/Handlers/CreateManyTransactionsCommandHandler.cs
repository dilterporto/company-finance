using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Company.Finance.Domain.Entities;
using Company.Finance.Persistence;
using MediatR;

namespace Company.Finance.Application.Transactions.Commands.Handlers
{
    public class CreateManyTransactionsCommandHandler : IRequestHandler<CreateManyTransactionsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateManyTransactionsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(CreateManyTransactionsCommand createManyTransactionsCommand, 
            CancellationToken cancellationToken)
        {
            var transactions = await Distinct(createManyTransactionsCommand.Transactions);
            
            await _unitOfWork.AddMany(transactions);
            await _unitOfWork.Commit();
            
            return Unit.Value;
        }

        private async Task<IEnumerable<Transaction>> Distinct(IEnumerable<Transaction> transactions)
        {
            var duplicatedTransactions = new List<Transaction>();
            var enumerable = transactions.ToList();
            foreach (var transaction in enumerable)
            {
                Expression<Func<Transaction, bool>> expression = x => x.Account.Equals(transaction.Account)
                                                                      && x.Amount == transaction.Amount && x.Currency == transaction.Currency
                                                                      && x.Date == transaction.Date && x.Memo == transaction.Memo && x.Type == transaction.Type;

                var isDuplicated = (await _unitOfWork.FindAll<Transaction>(expression)).Any();
                if (isDuplicated) duplicatedTransactions.Add(transaction);
            }

            return enumerable
                .Except(duplicatedTransactions, new TransactionEqualityComparer());
        }
    }
}