using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using Core.DTOs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<UserCreateDto> _validator;

        public UserController(IMediator mediator, IValidator<UserCreateDto> validator)
        {
            _mediator = mediator;
            _validator = validator;
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
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
            { 
                return BadRequest(validationResult.Errors);
            }

            var userId = await _mediator.Send(new CreateUserCommand(dto), cancellationToken);
            return CreatedAtAction(nameof(GetUserById), new { id = userId.Data }, new { Id = userId.Data });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserCreateDto dto, CancellationToken cancellationToken)
        {
            var updatedUserResult = await _mediator.Send(new UpdateUserCommand(id, dto), cancellationToken);
            return updatedUserResult.Data == Guid.Empty ? NotFound() : NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var deleteResult = await _mediator.Send(new RemoveUserCommand(id), cancellationToken);
            return deleteResult.Data ? NoContent() : NotFound();
        }
    }
}
