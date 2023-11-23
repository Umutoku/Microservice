using StackExchange.Redis;

namespace FreeEducation.Services.Basket.Services
{
    public class RedisService(string host, int port)
    {
        private ConnectionMultiplexer connectionMultiplexer;
        public void Connect()=> connectionMultiplexer
            = ConnectionMultiplexer.Connect($"{host}:{port}");

        public IDatabase GetDb(int db = 1) => connectionMultiplexer.GetDatabase(db);
    }
}
