using System.IO.MemoryMappedFiles;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Query.GetDishesForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Query.GetDishByIdForRestaurant;
internal class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger,
		IRestaurantsRepository restaurantsRepository,
		IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
	public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Retrieving dish: {DishId} for restaurant with: {RestaurantId}", 
			request.DishId,
			request.RestaurantId);
		var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
		if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

		var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

		var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
		if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());

		var result = mapper.Map<DishDto>(dish);
		return result;
	}
}
