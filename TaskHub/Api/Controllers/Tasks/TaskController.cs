using Api.Attributes.ModelBinders;
using Api.Controllers.Tasks.Request;
using Api.UseCases.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Api.Controllers.Tasks
{
    [Route("tasks")]
    [ApiController]
    public sealed class TaskController : ControllerBase
    {
        private readonly IManageTaskUseCase taskUseCase;
        public TaskController(IManageTaskUseCase _taskUseCase)
        {
            taskUseCase = _taskUseCase;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTasks(
            CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await taskUseCase.GetAllTasksAsync(cancellationToken);
                return Ok(tasks);
            }
            catch (Exception )
            {
                return StatusCode(500, "Ошибка при получении задач!");
            }
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(StudentInfoHeadersFilter))]
        [ServiceFilter(typeof(RequestLoggingFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaskById([FromRouteTaskId] Guid id,
        CancellationToken cancellationToken)
        {
            try
            {
                var response = await taskUseCase.GetTaskByIdAsync(id, cancellationToken);

                if (response == null)
                {
                    return NotFound($"Задача с id:{id} не найдена!");
                }

                return Ok(response);
            }
            catch (Exception )
            {
                return StatusCode(500, $"Ошибка при получении задачи с id:{id}!");
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(StudentInfoHeadersFilter))]
        [ServiceFilter(typeof(RequestLoggingFilter))]
        [ServiceFilter(typeof(ValidateCreateTaskRequestFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTaskAsync(
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
        {
            try
            {
                //хардкод
                var createdBy = Guid.Parse("62fd3021-9f6a-44df-8156-2062aa77607c");

                var response = await taskUseCase.CreateTaskAsync(request.Title, createdBy, cancellationToken);

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                //logger.log("Ошибка при создании задачи");
                Console.WriteLine($"\n\n{ex.Message}\n\n");
                return StatusCode(500, "Ошибка сервера при создании задачи!");
            }
        }

        [HttpPatch("{id}/title")]
        [ServiceFilter(typeof(StudentInfoHeadersFilter))]
        [ServiceFilter(typeof(RequestLoggingFilter))]
        [ServiceFilter(typeof(ValidateSetTaskTitleRequestFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RenameTask(
            [FromRouteTaskId] Guid id,
            [FromBody] SetTaskTitleRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var updated = await taskUseCase.SetTaskTitleAsync(
                            id,
                            request.Title,
                            cancellationToken);

                if (!updated)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                //logger.log("Ошибка в методе RenameTask!");
                return StatusCode(500, "Ошибка сервера при изменении названия задачи!");
            }
        }


        [HttpDelete("{id}")]
        [ServiceFilter(typeof(StudentInfoHeadersFilter))]
        [ServiceFilter(typeof(RequestLoggingFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTaskByIdAsync(
            [FromRouteTaskId] Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await taskUseCase.DeleteTaskAsync(id, cancellationToken);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, $"Ошибка сервера при удалении задачи с id:{id}!");
            }
        }

        [HttpDelete]
        [ServiceFilter(typeof(StudentInfoHeadersFilter))]
        [ServiceFilter(typeof(RequestLoggingFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllTasksAsync(
        CancellationToken cancellationToken)
        {
            try
            {
                await taskUseCase.DeleteAllTasksAsync(cancellationToken);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ошибка при удалении всех задач!");
            }
        }
    }
}
