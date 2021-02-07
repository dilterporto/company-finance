using System;
using Company.Finance.Models.Entities;
using Company.Finance.Models.ValueObjects;

namespace Company.Finance.Application.Transactions.Queries.Responses
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Memo { get; set; }
    }
}