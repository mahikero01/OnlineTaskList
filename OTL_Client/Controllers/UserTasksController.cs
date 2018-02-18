using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTL_Client.Models;
using OTL_Client.Models.ManageViewModels;

namespace OTL_Client.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UserTasksController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserTasksController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed
            };

            return View(model);
        }
    }
}