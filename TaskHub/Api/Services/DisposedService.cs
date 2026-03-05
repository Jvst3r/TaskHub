namespace Api.Services
{
    public class DisposedService : IDisposable
    {
        public Guid InstanceId { get; private set; } = Guid.NewGuid();
        public string ServiceName { get; private set; }
        public DisposedService(string _serviceName) 
        { 
            ServiceName = _serviceName ?? throw new ArgumentNullException("Service without name");
            //Вывод в консоль о создании
            Console.WriteLine(
                $"Created new service!" +
                $"name : {ServiceName}" +
                $"Id : {InstanceId}");
        }
        public void Dispose()
        {
            //вывод в консоль об окончании жизни инстанса
            Console.WriteLine(
                "Service was disposed!" +
                $"name : {ServiceName}" +
                $"Id : {InstanceId}");
        }
    }
}
