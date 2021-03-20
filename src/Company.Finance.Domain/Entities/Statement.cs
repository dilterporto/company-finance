using System;
using System.Collections.Generic;

namespace Company.Finance.Domain.Entities
{
    public class Statement
    {
        public Guid Id { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public decimal Balance { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}