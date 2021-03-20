using System.ComponentModel;

namespace Company.Finance.Domain.ValueObjects
{
    public enum TransactionType
    {
        [Description("Basic Credit")]
        Credit,
        [Description("Basic Debit")]
        Debit,
        Other,
    }
}