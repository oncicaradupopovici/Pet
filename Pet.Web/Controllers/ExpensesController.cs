using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pet.Application.Commands.ExpenseTracking;
using Pet.Application.Queries.ExpenseTracking;
using Pet.Application.Services;
using Pet.Web.Models;

namespace Pet.Web.Controllers
{
    [Route("api/[controller]")]
    public class ExpensesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IEventHub _eventHub;

        public ExpensesController(IMediator mediator, IEventHub eventHub)
        {
            _mediator = mediator;
            _eventHub = eventHub;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetExpenseList.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("{expenseId}/recipient")]
        public async Task<IActionResult> SetExpnenseRecipient([FromBody] SetExpenseRecipient.Command command)
        {
            await _mediator.Send(command);
            return Ok(SyncCommandResult.From(command, _eventHub.GetEvents()));
        }

        [HttpPost("{expenseId}/category")]
        public async Task<IActionResult> SetExpnenseCategory([FromBody] SetExpenseCategory.Command command)
        {
            await _mediator.Send(command);
            return Ok(SyncCommandResult.From(command, _eventHub.GetEvents()));
        }

        [HttpPost("{expenseId}/toSavings")]
        public async Task<IActionResult> MoveToSavings([FromBody] MoveExpenseToSavings.Command command)
        {
            await _mediator.Send(command);
            return Ok(SyncCommandResult.From(command, _eventHub.GetEvents()));
        }

        [HttpPost("cashExpense")]
        public async Task<IActionResult> AddCashExpense([FromBody] AddCashExpense.Command command)
        {
            await _mediator.Send(command);
            return Ok(SyncCommandResult.From(command, _eventHub.GetEvents()));
        }

    }
}
