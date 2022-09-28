using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Events
{
    public class UserCookieAuthenticationEvents : CookieAuthenticationEvents
    {

        private readonly ApplicationDbContext _context;

        public UserCookieAuthenticationEvents(ApplicationDbContext context)
        {
            _context = context;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            var userId = (from c in userPrincipal.Claims
                            where c.Type == ClaimTypes.NameIdentifier
                            select c.Value).FirstOrDefault();

            User user = await _context.User.FindAsync(Guid.Parse(userId));

            if (string.IsNullOrEmpty(userId) || user == null)
            {
                context.RejectPrincipal();

                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}