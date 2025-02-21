using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAgeRequirement
{
    public class MinimumAgeRequirement(int minimunAge) : IAuthorizationRequirement
    {
        public int MinimunAge { get; } = minimunAge;
    }
}
