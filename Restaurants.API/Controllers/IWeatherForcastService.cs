
namespace Restaurants.API.Controllers
{
	public interface IWeatherForcastService
	{
		IEnumerable<WeatherForecast> Get();
	}
}