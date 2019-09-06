﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pet.Application.Queries.ExpenseTracking;

namespace Pet.Web.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseRecipientsController : Controller
    {
        private readonly IMediator _mediator;

        public ExpenseRecipientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetExpenseRecipients.Query query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
