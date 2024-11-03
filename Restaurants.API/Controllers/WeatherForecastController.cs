using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{

	public class TemperatureRequest
	{
		public int Min { get; set; }
		public int Max { get; set; }
	}

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

		[HttpPost("Generate")]
		public IActionResult Generate([FromQuery] int count, [FromBody] TemperatureRequest request)
		{
			if (count < 0 || request.Max < request.Min)
			{
				return BadRequest("Count must be positive number, max value should be greater than min value");
			}

			var result = _weatherForcastService.Get(count, request.Min, request.Max);
			return Ok(result);
		}

	/*	[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			var result = _weatherForcastService.Get();
			return result;
		}
	*/
	}
}
