namespace Api.Services
{
    public interface ISingletonService2 : IHasInstanceId { }
    public class SingletonService2 : DisposedService, ISingletonService2
    {
        public SingletonService2() : base(nameof(SingletonService2)) { }
    }
}
