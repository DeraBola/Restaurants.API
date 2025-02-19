using System.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;


namespace Restaurants.Infrastructure.Seeders
{
	internal class RestaurantSeeder(RestaurantsDbContext _dbContext) : IRestaurantSeeder
	{

		public async Task Seed()
		{
			if (await _dbContext.Database.CanConnectAsync())
			{
				if (!_dbContext.Restaurants.Any())
				{
					var restaurants = GetRestaurants();
					_dbContext.Restaurants.AddRange(restaurants);
					await _dbContext.SaveChangesAsync();
				}

				if (!_dbContext.Roles.Any())
				{
					var roles = GetRoles();
					_dbContext.Roles.AddRange(roles);
					await _dbContext.SaveChangesAsync();
				}
			}
		}

		private IEnumerable<IdentityRole> GetRoles()
		{
			List<IdentityRole> roles =
				[
				new(UserRoles.User)
				{
					NormalizedName = UserRoles.User.ToUpper(),
				},
				new(UserRoles.Owner)
				{
					NormalizedName = UserRoles.Owner.ToUpper(),
				},
				new(UserRoles.Admin)
				{
					NormalizedName = UserRoles.Admin.ToUpper(),
				}
				];
			return roles;
		}

		private IEnumerable<Restaurant> GetRestaurants()
		{
			List<Restaurant> restaurants = new List<Restaurant>
			{
				new Restaurant
				{
					Name = "Gourmet Delights",
					Description = "A fine dining experience with a modern twist on classic flavors.",
					Category = "Fine Dining",
					HasDelivery = true,
					ContactEmail = "contact@gourmetdelights.com",
					ContactNumber = "123-456-7890",
					Address = new Address
					{
						City = "New York",
						Street = "123 Fancy Ave",
						PostalCode = "10001"
					},
					Dishes = new List<Dish>
					{
						new Dish
						{
							Name = "Truffle Pasta",
							Description = "Pasta with creamy truffle sauce, garnished with shaved truffles.",
							Price = 24.99m
						},
						new Dish
						{
							Name = "Seared Salmon",
							Description = "Pan-seared salmon with lemon butter sauce and asparagus.",
							Price = 29.99m
						}
					}
				},
				new Restaurant
				{
					Name = "Street Eats",
					Description = "Casual spot for quick and delicious street food from around the world.",
					Category = "Casual Dining",
					HasDelivery = false,
					ContactEmail = "info@streeteats.com",
					ContactNumber = "987-654-3210",
					Address = new Address
					{
						City = "Los Angeles",
						Street = "456 Market St",
						PostalCode = "90001"
					},
					Dishes = new List<Dish>
					{
						new Dish
						{
							Name = "Taco Trio",
							Description = "Three tacos with your choice of filling, topped with fresh salsa.",
							Price = 9.99m
						},
						new Dish
						{
							Name = "Loaded Nachos",
							Description = "Nachos topped with cheese, guacamole, jalapeños, and sour cream.",
							Price = 12.99m
						}
					}
				}
			};

			return restaurants;
		}
	}
}
