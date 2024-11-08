﻿using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;

namespace Restaurants.API.Controllers
{
	[Route("api/restaurants")]
	[ApiController]
	public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var restaurants = await restaurantsService.GetAllRestaurants();
				return Ok(restaurants);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var restaurant = await restaurantsService.GetRestaurantById(id);
			if(restaurant == null)
				return NotFound();
			return Ok(restaurant);
		}
	}
}
