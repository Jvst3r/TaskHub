namespace Api.Services
{
    public interface ITransientService2 : IHasInstanceId { }
    public class TransientService2 : DisposedService, ITransientService2
    {
        public TransientService2() : base(nameof(TransientService2)) { }
    }
}
