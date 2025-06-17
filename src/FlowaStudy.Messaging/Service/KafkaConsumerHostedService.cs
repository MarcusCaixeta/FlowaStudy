using Confluent.Kafka;
using FlowaStudy.Application.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FlowaStudy.Messaging.Service
{
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly IConfiguration _config;
        private readonly IKafkaConsumerHandler _handler;

        public KafkaConsumerHostedService(IConfiguration config, IKafkaConsumerHandler handler)
        {
            _config = config;
            _handler = handler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bootstrapServers = _config["Kafka:BootstrapServers"];
            var topic = _config["Kafka:Topic"];

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = "flowastudy-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            consumer.Subscribe(topic);

            Console.WriteLine("✅ Kafka consumer iniciado. Esperando mensagens...");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(TimeSpan.FromSeconds(2));

                    if (result != null)
                    {
                        Console.WriteLine($"📥 Mensagem recebida: {result.Message.Value}");
                        await _handler.HandleAsync(result.Message.Value);
                    }
                    else
                    {
                        Console.WriteLine("⏳ Nenhuma mensagem ainda...");
                    }

                    await Task.Delay(100, stoppingToken); // respira para não travar o host
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"⚠️ Erro ao consumir: {ex.Error.Reason}");
                    await Task.Delay(2000, stoppingToken); // backoff em caso de erro
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro inesperado: {ex.Message}");
                    await Task.Delay(5000, stoppingToken);
                }
            }

            consumer.Close();
        }

    }
}
