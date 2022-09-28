using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Helpers;
using ToDo.Models;
using ToDo.ViewModels;

namespace ToDo.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Guid userID = Guid
                .Parse(User
                    .FindFirst(ClaimTypes.NameIdentifier).Value);

            User user = _context.User.Find(userID);

            ProfileViewModel viewModel = new ProfileViewModel
            {
                Name = user.Name,
                Email = user.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Guid userID = Guid
                    .Parse(User
                        .FindFirst(ClaimTypes.NameIdentifier).Value);

                User user = _context.User.Find(userID);

                user.Name = viewModel.Name;
                user.UpdatedAt = DateTime.Now;

                if (!String.IsNullOrEmpty(viewModel.Password))
                {
                    user.Password = Hash.Create(viewModel.Password, Environment.GetEnvironmentVariable("AUTH_SALT"));
                }

                _context.User.Update(user);
                await _context.SaveChangesAsync();

                ViewBag.SuccessMessage = "Updated";
            }

            return View("../Profile/Index", viewModel);
        }
    }
}
