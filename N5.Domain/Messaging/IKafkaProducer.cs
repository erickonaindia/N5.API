namespace N5.Domain.Messaging
{
    public interface IKafkaProducer
    {
        Task SendMessageAsync(OperationDto messageDto, CancellationToken cancellationToken = default);
        Task ProduceAsync(OperationDto operation, CancellationToken cancellationToken = default);
    }
}
