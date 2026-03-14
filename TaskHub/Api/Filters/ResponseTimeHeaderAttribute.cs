using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Api.Filters
{
    public class ResponseTimeHeaderAttribute : ActionFilterAttribute
    {
        public ResponseTimeHeaderAttribute() { }

        public async InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();

            context.Response.OnStarting(() =>
            )
        }
    }
}
