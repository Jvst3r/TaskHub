using Api.Services;
namespace Api.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void ResolveAndCompare<TService>(this IServiceProvider provider) where TService : class, IHasInstanceId
        {
            var first = provider.GetRequiredService<TService>();
            var second = provider.GetRequiredService<TService>();

            Console.WriteLine("Type: " + typeof(TService).Name);

            Console.WriteLine("First: " + first.InstanceId);
            Console.WriteLine("Second: " + second.InstanceId);

            bool isServicesSame = ReferenceEquals(first, second);

            if (isServicesSame) Console.WriteLine("Services are same!");
            else Console.WriteLine("Different services!");

        }

    }
}
