
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares
{
	public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (BadRequestException badRequest)
			{
				context.Response.StatusCode = 400; // ✅ 400 Bad Request
				context.Response.ContentType = "application/json";

				var response = new { message = badRequest.Message };
				await context.Response.WriteAsJsonAsync(response);

				logger.LogWarning("Bad Request: {Message}", badRequest.Message);
			}
			catch (NotFoundException notFound)
			{
				context.Response.StatusCode = 404; // ✅ 404 Not Found
				context.Response.ContentType = "application/json";

				var response = new { message = notFound.Message };
				await context.Response.WriteAsJsonAsync(response);

				logger.LogWarning("Not Found: {Message}", notFound.Message);
			}
			catch (ForbidException)
			{
				context.Response.StatusCode = 403;
				await context.Response.WriteAsync("Forbidden access!");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Something went wrong");
			}
		}
	}
}
