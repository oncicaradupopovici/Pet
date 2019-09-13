using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet.Application.Commands.Imports;
using System.Threading.Tasks;

namespace Pet.Web.Controllers
{
    public class ImportViewModel
    {
        // other properties omitted

        public IFormFile File { get; set; }
        public string Bank { get; set; } = "ing";
    }

    [Route("api/[controller]")]
    public class ImportsController : Controller
    {
        private readonly IMediator _mediator;

        public ImportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("bankreport")]
        public async Task<IActionResult> ImportFromBankReport([FromForm]ImportViewModel import)
        {
            var cmd = new ImportFromBankReport.Command(import.File.OpenReadStream(), import.Bank);
            await _mediator.Send(cmd);
            return Ok();
        }

        [HttpPost("finq")]
        public async Task<IActionResult> ImportFromFinq([FromBody] ImportFromFinq.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
