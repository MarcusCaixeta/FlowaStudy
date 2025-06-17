using Confluent.Kafka;
using FlowaStudy.Application.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;

public class KafkaProducerService : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducerService(IConfiguration config)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public async Task SendAsync(string topic, string message)
    {
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
    }
}