using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Company.Finance.Application.Transactions.Queries;
using Company.Finance.Application.Transactions.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Company.Finance.Apis.Transactions
{
    [ApiController]
    [Route("transactions")]
    public class TransactionsController : Controller
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get transactions by date range
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<TransactionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NoContent)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            GetTransactionsByDateQuery query = (from, to);
            
            var transactions = await _mediator.Send(query);
            if (transactions.Any())
            {
                return Ok(transactions);
            }

            return NoContent();
        }
    }
}