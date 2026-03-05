namespace Api.Services
{
    public interface IScopedService2 : IHasInstanceId { }
    public class ScopedService2 : DisposedService, IScopedService2
    {
        public ScopedService2() : base(nameof(ScopedService2)) { }
    }
}
