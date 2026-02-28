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
            //ждём выполнение следующего этапа конвейера
            await next(context);

            watch.Stop();
            //добавление в заголовок кол-ва миллисекунд
            if (!context.Response.HasStarted)
                context.Response.Headers.Append("X-Response-Time-Ms", watch.ElapsedMilliseconds.ToString());
        }

    }
}
