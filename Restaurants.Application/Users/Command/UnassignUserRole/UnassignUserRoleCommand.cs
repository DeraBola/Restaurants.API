using MediatR;

namespace Restaurants.Application.Users.Command.UnassignUserRole
{
	public class UnassignUserRoleCommand: IRequest
	{
		public string UserEmail { get; set; } = default!;
	    public string RoleName { get; set; } = default!;

}
}
