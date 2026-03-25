using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes.Filters
{
    public class ValidateSetTaskTitleRequestFilter : ActionFilterAttribute
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
        }
    }
}
