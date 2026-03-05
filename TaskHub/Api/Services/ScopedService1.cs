namespace Api.Services
{
    public interface IScopedService1 : IHasInstanceId { }
    public class ScopedService1 : DisposedService, IScopedService1
    {
        public ScopedService1() : base(nameof(ScopedService1)) { }
    }
}
