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
                $"\nCreated new service!\n" +
                $"name : {ServiceName}\n" +
                $"Id : {InstanceId}");
        }
        public void Dispose()
        {
            //вывод в консоль об окончании жизни инстанса
            Console.WriteLine(
                "\nService was disposed!\n" +
                $"name : {ServiceName}\n" +
                $"Id : {InstanceId}");
        }
    }
}
