using System.Diagnostics;


namespace Api.Middleware
{
    /// <summary>
    /// Замеряет время обработки запроса. Также добавляет в ответ заголовок X-Response-Time-Ms. 
    /// </summary>
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Засекает время выполнения middleware и добавляет в Responce-заголовки это самое время
        /// </summary>
        /// <param name="context">HTTP-контекст</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            //засекаем время
            var watch = Stopwatch.StartNew();

            //v2.0
            context.Response.OnStarting(() =>
            {
                watch.Stop();
                context.Response.Headers.Append("X-Response-Time-Ms", watch.ElapsedMilliseconds.ToString());

                return Task.CompletedTask;
            });

            //ждём выполнение следующего этапа конвейера, заголовок будет добавлен благодаря делегату выше
            await next(context);
        }

    }
}
