using Api.Controllers.Tasks.Request;
using Api.UseCases.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Api.Controllers.Tasks
{
    [Route("tasks")]
    public sealed class TaskController : ControllerBase
    {
        private readonly IManageTaskUseCase taskUseCase;
        public TaskController(IManageTaskUseCase _taskUseCase)
        {
            taskUseCase = _taskUseCase;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTasks(
            [FromBody] CreateTaskRequest? request,
            CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await taskUseCase.GetAllTasksAsync(cancellationToken);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ошибка при получении задач!");
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid id,
        CancellationToken cancellationToken)
        {
            try
            {
                var response = taskUseCase.GetTaskByIdAsync(id, cancellationToken);

                if (response == null)
                {
                    return NotFound($"Задача с id:{id} не найдена!");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении задачи с id:{id}!");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTaskAsync(
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
        {
            //хардкод
            var createdBy = Guid.NewGuid();
            try
            {
                var response = await taskUseCase.CreateTaskAsync(request.Title, createdBy, cancellationToken);

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                //logger.log("Ошибка при создании задачи");
                return StatusCode(500, "Ошибка сервера при создании задачи!");
            }
        }

        [HttpPatch("{id:guid}/title")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RenameTask(
            [FromRoute] Guid id,
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
            catch (Exception ex)
            {
                //logger.log("Ошибка в методе RenameTask!");
                return StatusCode(500, "Ошибка сервера при изменении названия задачи!");
            }
        }


        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTaskByIdAsync(
            [FromRoute] Guid id,
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера при удалении задачи с id:{id}!");
            }
        }

        [HttpDelete]
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
            catch (Exception ex)
            {
                return StatusCode(500, "Ошибка при удалении всех задач!");
            }
        }
    }
}
