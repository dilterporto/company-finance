using AutoMapper;
using Company.Finance.Application.Statements.Commands;
using Company.Finance.Models.Entities;

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