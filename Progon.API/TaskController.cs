using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Progon.Application.Interfaces;

namespace Progon.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService) 
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult ListAll()
        {
            var tasks = _taskService.ListAll();

            return new ObjectResult(tasks);
        }

        //[HttpPost]
        //public IActionResult Create()
        //{
        //    var task = _taskService.CreateTask();

        //    return new ObjectResult(true);
        //}
    }
}
