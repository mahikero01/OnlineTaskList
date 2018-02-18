using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OTL_API.Services;
using OTL_Client.Models;
using OTL_Client.Models.ManageViewModels;

namespace OTL_Client.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UserTasksController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private AccessAPI _webAPI;

        public UserTasksController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _webAPI = new AccessAPI("http://localhost:50282/api/userTasks");
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            //List<UserTask> userTaskList;

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _webAPI.AssignAuth(user.UserName);
            var result = await _webAPI.GetRequest();
            var userTaskList = JsonConvert.DeserializeObject<UserTask[]>(result.ToString());

            return View(userTaskList);
        }

        public async Task<UserTask[]> GetData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _webAPI.AssignAuth(user.UserName);
            var result = await _webAPI.GetRequest();
            return JsonConvert.DeserializeObject<UserTask[]>(result.ToString());
        }
    }
}