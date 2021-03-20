using System;
using System.Collections.Generic;
using System.ComponentModel;
using Company.Finance.Domain.ValueObjects;

namespace Company.Finance.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Account Account { get; set; }
        public Currency Currency { get; set; }
        public TransactionType Type { get; set; }
        public DateTime? Date { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
    }

    public class TransactionEqualityComparer : IEqualityComparer<Transaction>
    {
        public bool Equals(Transaction x, Transaction y)
        {
            return x.Account.Equals(y.Account) 
                   && x.Currency == y.Currency 
                   && x.Type == y.Type 
                   && Nullable.Equals(x.Date, y.Date) 
                   && x.Amount == y.Amount 
                   && string.Equals(x.Memo, y.Memo, StringComparison.InvariantCulture);
        }

        public int GetHashCode(Transaction obj)
        {
            return obj.Account.AccountId.GetHashCode() ^
                   obj.Amount.GetHashCode() ^
                   obj.Currency.GetHashCode() ^
                   obj.Date.GetHashCode() ^
                   obj.Memo.GetHashCode() ^
                   obj.Type.GetHashCode();
        }
    }
    
    public enum Currency
    {
        [Description("Real")]
        BRL
    }
}