using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization
{
	public class RestaurantsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
	{
		public RestaurantsUserClaimsPrincipalFactory(
			UserManager<User> userManager,
			RoleManager<IdentityRole> roleManager,
			IOptions<IdentityOptions> options)
			: base(userManager, roleManager, options) { }

		public override async Task<ClaimsPrincipal> CreateAsync(User user)
		{
			var identity = await GenerateClaimsAsync(user);

			if (user.Nationality != null)
			{
				identity.AddClaim(new Claim("Nationality", user.Nationality));
			}

			if (user.DateofBirth != null)
			{
				identity.AddClaim(new Claim("DateofBirth", user.DateofBirth.Value.ToString("yyyy-MM-dd")));
			}

			return new ClaimsPrincipal(identity);		}
	}
}
