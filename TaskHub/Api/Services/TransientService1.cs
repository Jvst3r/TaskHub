namespace Api.Services
{
    public interface ITransientService1 : IHasInstanceId { }
    public class TransientService1 : DisposedService, ITransientService1
    {
        public TransientService1() : base(nameof(TransientService1)) { }
    }
}
