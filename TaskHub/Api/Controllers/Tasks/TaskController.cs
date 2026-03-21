using Api.Controllers.Tasks.Request;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await db.Tasks.ToArray();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ошибка при получении задач!");
            }
    }

        [HttpGet("tasks/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaskById([FromRoute]int id)
        {
            try
            {
                var task = db.Tasks;
                if (task == null)
                {
                    return NotFound($"Задача с id:{id} не найдена!");
                }
                return Ok(task);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "");
            }
        }

        [HttpPost("tasks/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            try 
            {
               
               return Created(task);
            }
            catch (Exception ex)
            {
                //logger.log("Ошибка при создании задачи");
                return StatusCode(500, "Ошибка сервера при создании задачи!");
            }
        }

        [HttpPatch("tasks/{id:guid}/title")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RenameTask([FromBody]SetTaskTitleRequest request)
        {
            try
            {

                return NoContent();
            }
            catch (Exception ex)
            {
                //logger.log("Ошибка в методе RenameTask!");
                return StatusCode(500, "Ошибка сервера при изменении названия задачи!");
            }
        }

}
