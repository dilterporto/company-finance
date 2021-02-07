using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Company.Finance.Apis.Statements.Messages
{
    public class StatementsIngestionRequest
    {
        public IList<IFormFile> StatementFiles { get; set; }
    }
}