using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pet.Application.Queries.ExpenseTracking;

namespace Pet.Web.Controllers
{
    [Route("api/[controller]")]
    public class AnalyticsController : Controller
    {
        private readonly IMediator _mediator;

        public AnalyticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("byCategory")]
        public async Task<IActionResult> GetExpenseByCategoryList([FromQuery] GetExpenseByCategoryList.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("byRecipient")]
        public async Task<IActionResult> GetExpenseByRecipientList([FromQuery] GetExpenseByRecipientList.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
