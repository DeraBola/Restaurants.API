
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
	public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
		IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
			MinimumAgeRequirement requirement)
		{
			var currentUser = userContext.GetCurrentUser();

			logger.LogInformation("User: { Email }, date of birth { DOB } - handling minimum requirement", 
				currentUser.Email, currentUser.)
		}
	}
}
