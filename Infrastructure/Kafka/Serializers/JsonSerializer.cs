using Confluent.Kafka;
using System.Text.Json;

namespace Infrastructure.Kafka.Serializers
{
    public class JsonSerializer<TMessage> : IAsyncSerializer<TMessage>
    {
        public async Task<byte[]> SerializeAsync(TMessage data, SerializationContext context)
        {
            using var memoryStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memoryStream, data);
            return memoryStream.ToArray();
        }
    }
}