using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTL_API.Services;
using Microsoft.Extensions.Logging;

namespace OTL_API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/UserTasks")]
    public class UserTasksController : Controller
    {
        private ILogger<UserTasksController> _logger;
        private IOnlineTaskListsRepository _repo;

        public UserTasksController(
                ILogger<UserTasksController> logger, 
                IOnlineTaskListsRepository repo )
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetUserTasks(int userID)
        {
            try
            {
                var usertasksResults = _repo.ReadUserTasks();

                return Ok(usertasksResults);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Error in GetUserTask : " + ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}