using System;
using System.Collections.Generic;
using Company.Finance.Domain.Entities;
using MediatR;

namespace Company.Finance.Application.Statements.Commands
{
    public class CreateStatementCommand : IRequest
    {
        public Guid Id { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public decimal Balance { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}