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
            //ждём выполнения конвейера
            await next(context);

            //добавляем заголовки с ФИО И академ группой
            if (!context.Response.HasStarted)
            {
                context.Response.Headers.Append("X-Student-Name", StudentName);
                context.Response.Headers.Append("X-Student-Group", StudentGroup);
            }
        }
    }
}
