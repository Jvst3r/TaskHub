using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Api.Controllers.Tasks
{
    public class TaskController : ControllerBase
    {
        private readonly DbContext db;
        public TaskController(DbContext _db) 
        {
            db = _db;
        }
        
        [HttpGet("tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await db.Tasks.ToArray();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Ошибка при получении задач!");
            }
    }

        [HttpGet("tasks/{id:guid}")]
        public async Task<IActionResult> GetTaskById([FromQuery]int id)
        {
            try
            {
                var task = db.Tasks;
                if (task == null)
                {
                    return NotFound($"Задача с id:{id} не найдена!")
                }
                return Ok(task);
            }
            catch (Exception ex) 
            {
                return StatusCode(400, "");
            }
        }

        [HttpPost("tasks/create")]
        public async Task<IActionResult> CreateTask([FromBody]Logic.Tasks.Task _task)
        {
            try 
            {
               var task = await db.Tasks.Add(_task);
               return Created(task);
            }
            catch (Exception ex)
            {
                //logger.log("Ошибка при создании задачи")
                return StatusCode(400, "Ошибка сервера при создании задачи!");
            }
        }

        [HttpPut("tasks/{id:guid}/title")]
        public async Task<IActionResult> RenameTask()

}
