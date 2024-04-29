using Confluent.Kafka;
using Microsoft.Extensions.Options;
using N5.Domain.Messaging;
using Newtonsoft.Json;

namespace N5.Infrastructure.Messaging
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public KafkaProducer(IOptions<KafkaConfig> config)
        {
            _bootstrapServers = config.Value.BootstrapServers;
            _topic = config.Value.Topic;
        }

        public async Task SendMessageAsync(Domain.Messaging.OperationDto messageDto, CancellationToken cancellationToken)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var message = new Message<Null, string>
                    {
                        Value = JsonConvert.SerializeObject(messageDto)
                    };

                    var deliveryResult = await producer.ProduceAsync(_topic, message, cancellationToken);

                    Console.WriteLine($"Produced message to: {deliveryResult.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> ex)
                {
                    Console.WriteLine($"Delivery failed: {ex.Error.Reason}");
                }
            }
        }

        public async Task ProduceAsync(Domain.Messaging.OperationDto operation, CancellationToken cancellationToken)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var message = new Message<Null, string>
                    {
                        Value = JsonConvert.SerializeObject(operation)
                    };

                    var deliveryResult = await producer.ProduceAsync(_topic, message, cancellationToken);

                    Console.WriteLine($"Produced message to: {deliveryResult.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> ex)
                {
                    Console.WriteLine($"Delivery failed: {ex.Error.Reason}");
                }
            }
        }
    }
}
