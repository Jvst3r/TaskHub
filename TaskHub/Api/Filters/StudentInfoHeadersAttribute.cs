using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class StudentInfoHeadersAttribute : ActionFilterAttribute
    {
        private static readonly string StudentName = "Pichugov Arseny";
        private static readonly string StudentGroup = "RI-240911";
        public StudentInfoHeadersAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.OnStarting(() =>
            {
                context.HttpContext.Response.Headers.Append("X-Student-Name", StudentName);
                context.HttpContext.Response.Headers.Append("X-Student-Group", StudentGroup);
                return Task.CompletedTask;
            });
        }
    }
}
