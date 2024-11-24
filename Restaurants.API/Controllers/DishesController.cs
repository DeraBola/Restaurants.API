using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Command.CreateDish;
using Restaurants.Application.Dishes.Command.DeleteDishes;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Query.GetDishByIdForRestaurant;
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
			var dishId = await mediator.Send(command);
			return CreatedAtAction(nameof(GetByIdForRestaurant), new {restaurantID, dishId }, null);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantID)
		{
			var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantID));
			return Ok(dishes);
		}

		[HttpGet("{dishId}")]
		public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantID, [FromRoute] int dishId)
		{
			var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantID, dishId));
			return Ok(dish);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantID)
		{
			 await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantID));
			return NoContent();
		}

	}
}
