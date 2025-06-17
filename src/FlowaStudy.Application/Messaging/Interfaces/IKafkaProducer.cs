namespace FlowaStudy.Application.Messaging.Interfaces
{
    public interface IKafkaProducer
    {
        Task SendAsync(string topic, string message);
    }
}
