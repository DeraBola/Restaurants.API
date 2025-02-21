﻿
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
			catch(NotFoundException notFound)
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync(notFound.Message);
				logger.LogWarning(notFound.Message);
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
