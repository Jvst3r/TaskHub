namespace Api.Middleware
{
    public class StudentInfoMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string StudentName = "Pichugov Arseny Sergeevich";
        private readonly string StudentGroup = "RI-240911";

        public StudentInfoMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Вставляет в Response-заголовки ФИО и группу студента(меня любимого)
        /// </summary>
        /// <param name="context">HTTP</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            //v2.0 
            //теперь не ждём выполнения, а делаем callback-делегат
            context.Response.OnStarting(() => 
                {
                context.Response.Headers.Append("X-Student-Name", StudentName);
                context.Response.Headers.Append("X-Student-Group", StudentGroup);
                return Task.CompletedTask;
                });

            
            await next(context);
        }
    }
}
