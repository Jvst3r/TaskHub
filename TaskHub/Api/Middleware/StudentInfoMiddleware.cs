namespace Api.Middleware
{
    public class StudentInfoMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string StudentName = "Пичугов Арсений Сергеевич";
        private readonly string StudentGroup = "РИ-240911";

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
            context.Response.Headers["X-Student-Name"] = StudentName;
            context.Response.Headers["X-Student-Group"] = StudentGroup;
        }
    }
}
