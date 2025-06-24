using FlowaStudy.Application.Messaging.Interfaces;
using System.Text.Json;

namespace FlowaStudy.Application.Messaging.Handler
{
    public class ProcessMessageHandler : IKafkaConsumerHandler
    {
        private readonly ILogRepository _logRepository;

        public ProcessMessageHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task HandleAsync(string message)
        {
            try
            {
                // Valida se o JSON é bem formado
                using var doc = JsonDocument.Parse(message);

                await _logRepository.SaveAsync("asset-transaction", message);
                Console.WriteLine("✅ Mensagem Kafka salva no Mongo.");
            }
            catch (JsonException)
            {
                Console.WriteLine("❌ JSON inválido recebido. Ignorado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro inesperado: {ex.Message}");
            }
        }
    }
}
