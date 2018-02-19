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
            List<UserTask> userTaskList = new List<UserTask>();
            IEnumerable<UserTask> userTaskListResultSet;

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _webAPI.AssignAuth(user.UserName);
            var result = await _webAPI.GetRequest();
            var deserializeResult = JsonConvert.DeserializeObject<UserTask[]>(result.ToString());

            foreach (var userTask in deserializeResult)
            {
                userTaskList.Add(userTask);
            }

            userTaskListResultSet = userTaskList.Where(ut => ut.UserID == user.Id);

            return View(userTaskListResultSet);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            UserTask userTaskResultSet;

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _webAPI.AssignAuth(user.UserName);
            var result = await _webAPI.GetRequest(id);
            var deserializeResult = JsonConvert.DeserializeObject<UserTask>(result.ToString());
            userTaskResultSet = deserializeResult;

            return View(userTaskResultSet);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            UserTask userTaskResultSet;

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _webAPI.AssignAuth(user.UserName);
            var result = await _webAPI.GetRequest(id);
            var deserializeResult = JsonConvert.DeserializeObject<UserTask>(result.ToString());
            userTaskResultSet = deserializeResult;

            return View(userTaskResultSet);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(UserTask userTask)
        {
            if (userTask == null)
            {

                return NotFound();
            }

            UserTaskForUpdateDTO userTaskUpdate = new UserTaskForUpdateDTO();
            userTaskUpdate.UserID = userTask.UserID;
            userTaskUpdate.Title = userTask.Title;
            userTaskUpdate.Description = userTask.Description;
            userTaskUpdate.IsDone = userTask.IsDone;
            var userTaskSerialize = JsonConvert.SerializeObject(userTaskUpdate);


            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _webAPI.AssignAuth(user.UserName);
            await _webAPI.PutRequest(userTask.UserTaskID.ToString(), userTaskSerialize);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserTask userTask)
        {
            if (userTask == null)
            {

                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            UserTaskForCreateDTO userTaskCreate = new UserTaskForCreateDTO();
            userTaskCreate.Title = userTask.Title;
            userTaskCreate.Description = userTask.Description;
            userTaskCreate.UserID = user.Id;
            var userTaskSerialize = JsonConvert.SerializeObject(userTaskCreate);

            _webAPI.AssignAuth(user.UserName);
            await _webAPI.PostRequest(userTaskSerialize);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _webAPI.AssignAuth(user.UserName);
            await _webAPI.DeleteRequest(id);

            return RedirectToAction(nameof(Index));
        }
    }
}