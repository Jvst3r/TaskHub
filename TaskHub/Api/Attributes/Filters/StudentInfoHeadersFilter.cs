using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes.Filters
{
    public class StudentInfoHeadersFilter : ActionFilterAttribute
    {
        private readonly static string StudentName = "Pichugov Arseny Sergeich";
        private readonly static string StudentGroup = "ri-240911";

        //использую ResultExecuting потому что добавляем заголовки после отработки эндпоинта
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers["X-Studenr-Group"] = StudentGroup;
            context.HttpContext.Response.Headers["X-Student-Name"] = StudentName;
        }
    }
}
