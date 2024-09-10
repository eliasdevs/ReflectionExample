using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReflectionExample.Commands;
using ReflectionExample.Dtos;

namespace ReflectionExample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestCommandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TestCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("One")]
        public async Task<IActionResult> CommandOne(CommandData1 data1)
        {
            await _mediator.Send(new CommandExample1Unit(data1));
            return NoContent();
        }

        [HttpPost("Two")]
        public async Task<IActionResult> CommandTwo(CommandData1 data1)
        {
            var result = await _mediator.Send(new CommandExample1(data1));
            return Ok(result);
        }

    }
}
