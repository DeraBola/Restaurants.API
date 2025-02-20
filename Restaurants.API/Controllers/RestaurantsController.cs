using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("api/restaurants")]
	//[Authorize]
	public class RestaurantsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		[Authorize(Policy = PolicyNames.Atleast20)]
		//[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>)]
		public async Task<IActionResult> GetAll()
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized("User ID not found in claims.");
			}
			 
			var restaurants = await mediator.Send(new GetAllRestaurantsQuery(userId));	
			return Ok(restaurants);
		}

		[HttpGet("{id}")]
		//[AllowAnonymous]
		[Authorize(Policy = PolicyNames.HasNationality)]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
			return Ok(restaurant);
		}


		[HttpPost]
		[Authorize(Roles = UserRoles.Owner)]
		public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createCommand)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			int id = await mediator.Send(createCommand);
			return CreatedAtAction(nameof(GetById), new { id }, null);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
		{
			command.Id = id;

			await mediator.Send(command);

			return NotFound();
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
		{
		 await mediator.Send(new DeleteRestaurantCommand(id));

		 return NoContent();
		}

	}
}
