﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
	public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
		IMapper mapper,
		IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
	{
		public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all Restaurants {RestaurantId}", request.Id);
			var restaurant = await restaurantsRepository.GetByIdAsync(request.Id) 
				?? throw new BadRequestException("Restaurant not found.");

			var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
			return restaurantDto;
		}
	}
}
