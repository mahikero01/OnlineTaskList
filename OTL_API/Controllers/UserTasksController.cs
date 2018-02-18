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
using OTL_API.Models;
using Microsoft.AspNetCore.Cors;

namespace OTL_API.Controllers
{
    [Authorize]
    [EnableCors("AllowWebClient")]
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

        //GET: api/UserTasks
        [HttpGet]
        public IActionResult GetUserTasks()
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

        //GET: api/UserTasks/{id}
        [HttpGet("{id}", Name = "GetUserTask")]
        public IActionResult GetUserTask(Guid id)
        {
            var userTaskResult = _repo.ReadUserTask(id);

            if (userTaskResult == null)
            {
                return NotFound();
            }

            return Ok(userTaskResult);
        }

        //POST: api/UserTasks
        [HttpPost()]
        public IActionResult PostUserTask([FromBody] UserTaskForCreateDTO userTask)
        {
            if (userTask == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUserTaskEntity = Mapper.Map<Entities.UserTask>(userTask);

            _repo.CreateUserTask(newUserTaskEntity);

            if (!_repo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtRoute("GetUserTask",
                    new { id = newUserTaskEntity.UserTaskID }, newUserTaskEntity);
        }

        //PUT: api/UserTasks
        [HttpPut("{id}")]
        public IActionResult PutUserTask(Guid id, [FromBody] UserTaskForUpdateDTO userTask)
        {
            if (userTask == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTaskEntity = _repo.ReadUserTask(id);
            if (userTaskEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(userTask, userTaskEntity);

            if (!_repo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        ////DELETE: api/UserTask/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUserTask(Guid id)
        {
            var userTaskEntity = _repo.ReadUserTask(id);
            if (userTaskEntity == null)
            {
                return NotFound();
            }

            _repo.DeleteUserTask(userTaskEntity);

            if (!_repo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}