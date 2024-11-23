using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Command.CreateDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Query.GetDishesForRestaurant;

namespace Restaurants.API.Controllers
{
	[Route("api/restaurant/{restaurantID}/dishes")]
	[ApiController]
	public class DishesController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateDish([FromRoute] int restaurantID, CreateDishCommand command)
		{
			command.RestaurantId = restaurantID;
			await mediator.Send(command);
			return Created();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantID)
		{
			var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantID));
			return Ok(dishes);
		}

		/* [HttpGet]
		public async Task<ActionResult<IEnumerable<DishDto>>> GetByIdForRestaurant([FromRoute] int restaurantID)
		{
			var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantID));
			return Ok(dishes);
		}
		*/
	}
}
