using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pet.Application.Queries.ExpenseTracking;

namespace Pet.Web.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseCategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public ExpenseCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetExpenseCategories.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
