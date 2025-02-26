using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using System.Security.Claims;

namespace Restaurants.Application.Users.Tests
{
	public class UserContextTests
	{
		[Fact()]
		public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
		{
			// arrange
			var dateOfBirth = new DateOnly(1990, 1, 1);

			var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
			var claims = new List<Claim>()
			{
				new(ClaimTypes.NameIdentifier, "1"),
				new(ClaimTypes.Email, "test@test.com"),
				new(ClaimTypes.Role, UserRoles.Admin),
				new(ClaimTypes.Role, UserRoles.User),
				new("Nationality", "German"),
				new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
			};

			var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

			httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
			{
				User = user
			});

			var userContext = new UserContext(httpContextAccessorMock.Object);

			// act
			var currentUser = userContext.GetCurrentUser();
			Console.WriteLine(string.Join(", ", currentUser.Roles));


			// asset

			currentUser.Should().NotBeNull();
			currentUser.Id.Should().Be("1");
			currentUser.Email.Should().Be("test@test.com");
			currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
			// currentUser.Roles.Should().BeEquivalentTo(new[] { UserRoles.Admin, UserRoles.User });
			currentUser.Nationality.Should().Be("German");
			currentUser.DateOfBirth.Should().Be(dateOfBirth);

		}
	}
}