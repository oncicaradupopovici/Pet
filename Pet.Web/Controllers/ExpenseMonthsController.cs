using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pet.Application.Queries.ExpenseTracking;

namespace Pet.Web.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseMonthsController : Controller
    {
        private readonly IMediator _mediator;

        public ExpenseMonthsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromRoute] GetExpenseMonthList.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetList([FromRoute] GetCurrentExpenseMonth.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
