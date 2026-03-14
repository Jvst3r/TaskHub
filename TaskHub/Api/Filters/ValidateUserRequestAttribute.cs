using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Api.Filters
{
    public class ValidateUserRequestAttribute : ActionFilterAttribute
    {
        public ValidateUserRequestAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Обработка пустого запроса
            var request = context.ActionArguments.Values.FirstOrDefault();
            if (request == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }
            
            //обработка имени через рефлексию
            var property = request.GetType().GetProperty("Name");
            var name = property?.GetValue(request) as string;

            if (string.IsNullOrWhiteSpace(name))
            {
                context.Result = new BadRequestObjectResult("Имя пользователя не задано");
            }
        }
    }
}
