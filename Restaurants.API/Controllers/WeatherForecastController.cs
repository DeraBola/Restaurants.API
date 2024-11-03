using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WeatherForecastController : ControllerBase
	{

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IWeatherForcastService _weatherForcastService;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForcastService weatherForcastService)
		{
			_logger = logger;
			_weatherForcastService = weatherForcastService;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			var result = _weatherForcastService.Get();
			return result;
		}
	}
}
