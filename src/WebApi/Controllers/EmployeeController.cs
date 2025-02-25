using MediatR;
using Microsoft.AspNetCore.Mvc;
using WEB_API_CQRS.src.Application.Commands.Employees;
using WEB_API_CQRS.src.Application.Queries.Employees;
using WEB_API_CQRS.src.Domain.Entities;

namespace WEB_API_CQRS.src.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAll()
        {
            var products = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _mediator.Send(new GetEmployeeQuery { Id = id });
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        {
            var product = await _mediator.Send(command);

            if (product == null)
            {
                return BadRequest("Failed to create the employee.");
            }

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL and request body must match.");
            }

            var updatedEmployee = await _mediator.Send(command);
            return updatedEmployee == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
            return result ? NoContent() : NotFound();
        }
    }
}
