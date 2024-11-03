namespace Restaurants.API.Controllers
{
	public class WeatherForcastService : IWeatherForcastService
	{
		private static readonly string[] Summaries = new[]
	{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		public IEnumerable<WeatherForecast> Get(int count, int minTemperatre, int maxTemperature)
		{
			return Enumerable.Range(1, count).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(minTemperatre, maxTemperature),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}
