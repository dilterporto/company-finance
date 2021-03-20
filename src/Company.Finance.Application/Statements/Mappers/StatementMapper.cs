using AutoMapper;
using Company.Finance.Application.Statements.Commands;
using Company.Finance.Domain.Entities;

namespace Company.Finance.Application.Statements.Mappers
{
    public class StatementMapper : Profile
    {
        public StatementMapper()
        {
            CreateMap<CreateStatementCommand, Statement>();
        }
    }
}