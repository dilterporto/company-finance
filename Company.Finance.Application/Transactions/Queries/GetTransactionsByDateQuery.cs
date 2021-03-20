using System;
using System.Collections.Generic;
using Company.Finance.Application.Transactions.Queries.Responses;
using MediatR;

namespace Company.Finance.Application.Transactions.Queries
{
    public class GetTransactionsByDateQuery : IRequest<IEnumerable<TransactionResponse>>
    {
        public DateRangeFilter Filter { get; set; }

        public static implicit operator GetTransactionsByDateQuery((DateTime? from, DateTime? to) filter)
        {
            return new()
            {
                Filter = new DateRangeFilter
                {
                    From = filter.from,
                    To = filter.to
                }
            };
        }
    }

    public class DateRangeFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}