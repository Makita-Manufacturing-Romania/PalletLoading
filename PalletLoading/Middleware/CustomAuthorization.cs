using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PalletLoading.Helpers;
using PalletLoading.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace Part_Center_Systems.Middleware
{
    public class CustomAuthorization : AuthorizationHandler<RoleRequirement>
    {
        private readonly PalletLoadingContext _context;

        public CustomAuthorization(PalletLoadingContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name && c.Issuer == "AD AUTHORITY"))
            {
                return Task.FromResult(0);
            }

            var username = context.User.FindFirst(c => c.Type == ClaimTypes.Name && c.Issuer == "AD AUTHORITY").Value.Replace("MMRMAKITA\\", "");
            var roles = requirement.RoleNames;
            var roleId = 999;

            //UserName Check
            var user = _context.User.Where(r => r.Username == username).FirstOrDefault();
            string errorMessage = "";

            if (user == null)
            {
                errorMessage = "Nume de utilizator incorect sau inexistent!";
                return Task.FromResult(0);//SENDS ME TO ERROR PAGE
            }

            //Email Check
            if (username != user.Username)
            {
                errorMessage = "Email de utilizator incorect sau inexistent!";
                return Task.FromResult(0);//SENDS ME TO ERROR PAGE

            }

            var userId = user.Username;

            foreach (var role in roles)
            {
                roleId = _context.Role.First(r => r.Name == role).Id;
                if (_context.User.FirstOrDefault(u => u.Username == userId).RoleId == roleId)
                {
                    context.Succeed(requirement);
                    return Task.FromResult(0);
                }
            }

            errorMessage = "Rolul tau nu are drepturi pentru a accesa aceasta pagina!";
            return Task.FromResult(0);//SENDS ME TO ERROR PAGE
        }
    }
}
