using System.Collections.Generic;
using Company.Finance.Application.Statements.Commands;
using Company.Finance.Models.Entities;
using MediatR;

namespace Company.Finance.Application.Transactions.Commands
{
    public class CreateManyTransactionsCommand : IRequest
    {
        public IList<Transaction> Transactions { get; set; }

        public static CreateManyTransactionsCommand CreateFrom(CreateStatementCommand createStatementCommand)
        {
            return new()
            {
                Transactions = createStatementCommand.Transactions
            };
        }
    }
}