using FlowaStudy.Application.Messaging.Interfaces;

namespace FlowaStudy.Application.Messaging.Handler
{
    public class ProcessMessageHandler : IKafkaConsumerHandler
    {
        public Task HandleAsync(string message)
        {
            Console.WriteLine($"Mensagem recebida: {message}");
            return Task.CompletedTask;
        }
    }
}
