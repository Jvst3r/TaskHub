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

            var body = context.HttpContext.Request.Body;
            if (body == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }

            var id = (Guid)context.ActionArguments["id"];
            if (id == Guid.Empty || 
                string.IsNullOrEmpty(id.ToString()))
            {
                context.Result = new BadRequestObjectResult("Идентификатор пользователя не задан");
                return;
            }

            var title = (string)context.ActionArguments["title"];
            if (string.IsNullOrEmpty(title))
            {
                context.Result = new BadRequestObjectResult("Название задачи не задано");
                return;
            }
        }
    }
}
