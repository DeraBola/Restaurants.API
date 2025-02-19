using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Command.AssignUserRole;
using Restaurants.Application.Users.Command.UnassignUserRole;
using Restaurants.Application.Users.Command.UpdateUserDetails;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [Route("api/identity/user")]
	[ApiController]
	public class IdentityController(IMediator mediator) : ControllerBase
	{
		[HttpPatch]
		[Authorize]
		public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
		{
			Console.WriteLine($"Received Request: {JsonSerializer.Serialize(command)}");
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await mediator.Send(command);
			return NoContent();
		}

		[HttpPost("userRole")]
		[Authorize(Roles = UserRoles.Admin)]
		public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
		{
			Console.WriteLine($"Received Request: {JsonSerializer.Serialize(command)}");
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("deleteRole")]
		[Authorize(Roles = UserRoles.Admin)]
		public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
		{
			Console.WriteLine($"Received Request: {JsonSerializer.Serialize(command)}");

			await mediator.Send(command);
			return NoContent();
		}
	}
}
