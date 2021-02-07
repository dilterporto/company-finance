using System.IO;
using AutoMapper;
using Company.Finance.Application.Statements.Commands;
using Microsoft.AspNetCore.Http;

namespace Company.Finance.Apis.Statements.Mappers
{
    public class StatementRequestMapper : Profile
    {
        public StatementRequestMapper()
        {
            CreateMap<IFormFile, IngestStatementFileCommand>()
                .ForMember(x => x.Stream,
                    opt => 
                        opt.MapFrom(x => x.OpenReadStream()))
                .ForMember(x => x.Extension, 
                    opt => 
                        opt.MapFrom(x => Path.GetExtension(x.FileName)));
                
        }
    }
}