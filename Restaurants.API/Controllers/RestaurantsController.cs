using System.Reflection.Metadata.Ecma335;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
	[Route("api/restaurants")]
	//[ApiController]
	public class RestaurantsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var restaurants = await mediator.Send(new GetAllRestaurantsQuery());	
				return Ok(restaurants);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));


			if(restaurant == null)
				return NotFound();
			return Ok(restaurant);
		}

		[HttpPost]
		public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createCommand)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			int id = await mediator.Send(createCommand);
			return CreatedAtAction(nameof(GetById), new { id }, null);
		}
	}
}
