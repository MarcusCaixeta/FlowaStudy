namespace FlowaStudy.Application.Messaging.Interfaces
{
    public interface IKafkaConsumerHandler
    {
        Task HandleAsync(string message);
    }
}
