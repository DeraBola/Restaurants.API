using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Command;

namespace Restaurants.API.Controllers
{
	[Route("api/Identity/user")]
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
	}
}
