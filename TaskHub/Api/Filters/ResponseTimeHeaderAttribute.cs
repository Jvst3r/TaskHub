using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Linq;

namespace Api.Filters
{
    public class ResponseTimeHeaderAttribute : ActionFilterAttribute
    {
        public ResponseTimeHeaderAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //запускаем часы
            var watch = new Stopwatch();
            watch.Start();
            //сначала думал писать callback в OnActionExecuted, потом понял что работает замыкание, если написать здесь:)
            //context.HttpContext.Items["Watch"] = watch;

            //callback
            context.HttpContext.Response.OnStarting(() =>
            {
               //Stopwatch watch = (Stopwatch)context.HttpContext.Items["Watch"];
                watch.Stop();

                context.HttpContext.Response.Headers.Append("X-Response-Time-Ms", watch.ElapsedMilliseconds.ToString());
                return Task.CompletedTask;
            });
        }
        //можно не переопределять, но я уже написал  ¯\_(ツ)_/¯
        public override void OnActionExecuted(ActionExecutedContext context) => base.OnActionExecuted(context);
        
    }
}
