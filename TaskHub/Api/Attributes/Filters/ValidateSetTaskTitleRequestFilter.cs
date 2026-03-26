using Api.Controllers.Tasks.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes.Filters
{
    public class ValidateSetTaskTitleRequestFilter : ActionFilterAttribute
    {
        //использую ActionExecuting для валидации до начала выполнения контроллера
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //посмотрел что приходит, оказывается всё время это было не body, а request
            object request;
            context.ActionArguments.TryGetValue("request",out request);

            var dto = (SetTaskTitleRequest)request;

                if (dto == null || string.IsNullOrEmpty(dto.Title))
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }
        }
    }
}
