﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IRestaurantsService, RestaurantsService>();
		}
	}
}
