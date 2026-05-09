using Api.Controllers.Tasks.Request;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes.Filters
{
    public class ValidateCreateTaskRequestFilter : ActionFilterAttribute
    {
        //использую ActionExecuting для валидации до начала выполнения контроллера
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue("request", out var requestObj))
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }

            var dto = (CreateTaskRequest) requestObj;
            var id = dto.UserId;

            if (id == Guid.Empty || 
                string.IsNullOrEmpty(id.ToString()))
            {
                context.Result = new BadRequestObjectResult("Идентификатор пользователя не задан");
                return;
            }

            var title = dto.Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                context.Result = new BadRequestObjectResult("Название задачи не задано");
                return;
            }
        }
    }
}
