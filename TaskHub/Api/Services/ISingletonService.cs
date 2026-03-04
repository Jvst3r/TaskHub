namespace Api.Services
{
    public interface ISingletonService1 : IHasInstanceId { }
    public class ISingletonService1 : DisposedService, ISingletonService1
    {

    }
}
