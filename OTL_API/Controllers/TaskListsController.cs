using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTL_API.Services;
using OTL_API.Models;

namespace OTL_API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/TaskLists")]
    public class TaskListsController : Controller
    {
        private IOnlineTaskListRepository _repo;

        public TaskListsController(IOnlineTaskListRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{userID}")]
        public IActionResult GetUserTasks(int userID)
        {
            try
            {
                
                if (!_repo.UserIDExist(userID))
                {
                    return NotFound();
                }


                var usertaskListResults =
                        Mapper.Map<IEnumerable<TaskListDTO>>(_repo.ReadUserTasks(userID));

                return Ok(usertaskListResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

        }
    }
}