using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return Ok(users);
        }

        [HttpGet("with-products")]
        public async Task<IActionResult> GetAllUsersWithProducts(CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetAllUsersWithProductsQuery(), cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpGet("{id:guid}/with-products")]
        public async Task<IActionResult> GetUserByIdWithProducts(Guid id, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdWithProductsQuery(id), cancellationToken);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new CreateUserCommand(dto), cancellationToken);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, new { Id = userId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserCreateDto dto, CancellationToken cancellationToken)
        {
            var updatedUserId = await _mediator.Send(new UpdateUserCommand(id, dto), cancellationToken);
            return updatedUserId == Guid.Empty ? NotFound() : NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var isDeleted = await _mediator.Send(new RemoveUserCommand(id), cancellationToken);
            return isDeleted ? NoContent() : NotFound();
        }
    }
}
