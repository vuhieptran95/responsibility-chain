using MessagePack;

namespace ResponsibilityChain.Business.Caching
{
    public class CacheResponse<TResponse>
    {
        private byte[] _bytes;

        public void SetResponse(TResponse response)
        {
            _bytes = MessagePackSerializer.Serialize(response,
                MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block));
        }

        public TResponse GetResponse()
        {
            return MessagePackSerializer.Deserialize<TResponse>(_bytes,
                MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block));
        }
    }
}