
using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
	public class GetAllRestaurantsQuery(int id, string userId): IRequest<IEnumerable<RestaurantDto>>
	{
		public int Id { get; } = id;
		public string UserId { get; set; } = userId;
	}
}
