namespace Api.Services
{
    public interface ISingletonService1 : IHasInstanceId { }
    public class SingletonService1 : DisposedService, ISingletonService1
    {
        public SingletonService1() : base(nameof(SingletonService1)) { }
    }
}
