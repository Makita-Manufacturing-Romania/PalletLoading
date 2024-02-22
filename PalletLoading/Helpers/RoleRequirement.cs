using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace PalletLoading.Helpers
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string[] roles)
        {
            foreach (var role in roles)
            {
                RoleNames.Add(role);
            }
        }
        public ICollection<string> RoleNames { get; set; } = new List<string>();
    }
}
