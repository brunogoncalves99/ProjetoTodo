using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Helpers;
using ToDo.Models;
using ToDo.ViewModels;

namespace ToDo.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoRegistration(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Password = Hash.Create(viewModel.Password, Environment.GetEnvironmentVariable("AUTH_SALT")),
                    CreateAt = DateTime.Now
                };

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                List<Claim> claims = new List<Claim>{
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            return View("../Register/Index", viewModel);
        }
    }
}
