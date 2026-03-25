using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Api.Attributes.Filters
{
    public class RequestLoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger logger;
        private readonly Stopwatch watch;
        public RequestLoggingFilter(ILogger _logger)
        {
            logger = _logger;
            watch = new Stopwatch();
        }

        //использую ActionExecuting чтобы начать замер времени и написать лог о старте выполнения эндпоинта
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            watch.Start();
            
            var method = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path;

            logger.LogInformation($"Начато выполнение метода {method} с путём {path}");
        }

        //использую ResultExecuting чтобы добавить в лог после завершения выполнения эндпоинта
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            watch.Stop();

            var statusCode = context.HttpContext.Response.StatusCode;
            var method = context.HttpContext.Request.Method;
            var time =watch.ElapsedMilliseconds.ToString();

            logger.LogInformation($"Метод {method} завершён со статус кодом {statusCode} за {time} миллисекунд");
        }
    }
}
