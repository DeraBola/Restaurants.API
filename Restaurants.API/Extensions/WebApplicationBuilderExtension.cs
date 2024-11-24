﻿using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions
{
	public static class WebApplicationBuilderExtension
	{
		public static void AddPresentation(this WebApplicationBuilder builder)
		{
			builder.Services.AddAuthentication();
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
				{
				new OpenApiSecurityScheme
				{
				 Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
				},
				[]
				}
				});
			});

			builder.Services.AddScoped<ErrorHandlingMiddleware>();
			builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
			builder.Host.UseSerilog((contest, configuration) =>
             configuration
            .ReadFrom.Configuration(contest.Configuration)
             );
		}
	}
}
