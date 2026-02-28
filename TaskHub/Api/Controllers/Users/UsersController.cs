using Api.Controllers.Users.Request;
using Api.Controllers.Users.Response;
using Api.UseCases.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users;

/// <summary>
/// Контроллер работы с пользователями
/// </summary>
[ApiController]
[Route("users")]
public sealed class UsersController : ControllerBase
{
    /// <summary>
    /// UseCase управления пользователями
    /// </summary>
    private readonly IManageUserUseCase _userUseCase;

    public UsersController(IManageUserUseCase userUseCase)
    {
        _userUseCase = userUseCase;
    }



    //Короче я не знаю почему у меня ничего не работает, из-за того, что у Докера сегодня выходной
    // Этот ендпоинт мне сгенерировала ИИшка чтобы просто затестить написанные middleware
    // извиняюсь, но я чет 2 часа сижу туплю, устал уже, ничего не помогает
    // К следующей домашке установлю докер заново в 200-ый раз и он заработает
    // не снижайте баллы, пожалуйста :)
    [HttpGet("ping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Ping()
    {
        return Ok(new
        {
            status = "ok",
            message = "Middleware test",
            timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="request">Данные для создания пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный пользователь</returns>
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUserAsync(
        [FromBody] CreateUserRequest? request,
        CancellationToken cancellationToken)
    {
        var user = await _userUseCase.CreateUserAsync(request!.Name, cancellationToken);
        return Ok(user);
    }

    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список пользователей</returns>
    [HttpGet]
    public async Task<ActionResult<UserListResponse>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var response = await _userUseCase.GetAllUsersAsync(cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь или 404, если пользователь не найден</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userResponse = await _userUseCase.GetUserByIdAsync(id, cancellationToken);
        
        if (userResponse is null)
        {
            return NotFound();
        }

        return Ok(userResponse);
    }

    /// <summary>
    /// Установить имя пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="request">Данные для установки имени</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>204 при успехе или 400 при неверном запросе</returns>
    [HttpPut("{id:guid}/name")]
    public async Task<IActionResult> SetUserNameAsync(
        [FromRoute] Guid id,
        [FromBody] SetUserNameRequest? request,
        CancellationToken cancellationToken)
    {
        await _userUseCase.SetUserNameAsync(id, request!.Name!, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Удалить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>204 при успехе или 404, если пользователь не найден</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _userUseCase.DeleteUserByIdAsync(id, cancellationToken);
        if (deleted == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить всех пользователей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>204 при успехе</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteAllUsersAsync(CancellationToken cancellationToken)
    {
        await _userUseCase.DeleteAllUsersAsync(cancellationToken);
        return NoContent();
    }
}