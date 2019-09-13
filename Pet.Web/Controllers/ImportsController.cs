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
    }

    [Route("api/[controller]")]
    public class ImportsController : Controller
    {
        private readonly IMediator _mediator;

        public ImportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> ImportFromBankReport([FromForm]ImportViewModel import)
        {
            var cmd = new ImportFromBankReport.Command(import.File.OpenReadStream());
            await _mediator.Send(cmd);
            return Ok();
        }
    }
}
