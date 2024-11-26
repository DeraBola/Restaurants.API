
using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
	public class GetAllRestaurantsQuery(string userId) : IRequest<IEnumerable<RestaurantDto>>
	{
		public string UserId { get; set; } = userId;
	}
}
