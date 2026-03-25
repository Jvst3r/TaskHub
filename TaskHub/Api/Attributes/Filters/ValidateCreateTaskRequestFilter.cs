using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes.Filters
{
    public class ValidateCreateTaskRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var body = context.HttpContext.Request.Body;

            if (body == null )
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }

            var id = body.
        }
    }
}
