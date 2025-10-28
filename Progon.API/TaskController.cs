using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Progon.Application.Interfaces;
using Progon.Domain.Entities;
using Progon.Domain.Enums;

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

        //Listagem total
        [HttpGet("list")]
        public IActionResult ListAll()
        {
            var tasks = _taskService.ListAll();

            return new ObjectResult(tasks);
        }

        //Listagem com filtro
        [HttpGet("listWithFilter")]
        public IActionResult ListWithFilter(string name = null, OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tasks = _taskService.ListWithFilter(name, status, startDate, endDate);

            return new ObjectResult(tasks);
        }

        //Criação de task
        [HttpPost("createTask")]
        //Feito com parametros para testes, atualizar para receber json
        public IActionResult Create(string name, string description, TypeTask type, DateTime createDate, DateTime startDate, DateTime? finishDate, OrderStatus status)
        {
            var taskCreate = new SimpleTask(name, description, type, createDate, startDate, finishDate, status);
            var task = _taskService.CreateTask(taskCreate);

            return new ObjectResult(true);
        }
    }
}
