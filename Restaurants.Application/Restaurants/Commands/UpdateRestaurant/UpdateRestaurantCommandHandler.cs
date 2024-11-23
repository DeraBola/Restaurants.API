﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
		IRestaurantsRepository restaurantsRepository,
		IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
{
	public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

		var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
		if (restaurant is null)
			throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

		mapper.Map(request, restaurant);

		await restaurantsRepository.SaveChanges();
	}
}
