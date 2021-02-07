using System;
using System.Globalization;
using AutoMapper;
using Company.Finance.Application.Transactions.Queries.Responses;
using Company.Finance.Models.Entities;

namespace Company.Finance.Application.Transactions.Mappers
{
    public class TransactionMapper : Profile
    {
        public TransactionMapper()
        {
            CreateMap<Transaction, TransactionResponse>()
                .ForMember(x => x.Date, opt =>
                {
                    opt.MapFrom(x => x.Date.Value.ToString("dd/MM/yyyy hh:mm:ss"));
                })
                .ForMember(x => x.Amount, opt =>
                {
                    opt.MapFrom(x => x.Amount.ToString("C", new CultureInfo("pt-BR")));
                });
        }
    }
}