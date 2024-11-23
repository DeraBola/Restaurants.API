using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Command.CreateDish;

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
	}
}
