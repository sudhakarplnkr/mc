namespace MicroCredential.Infrastructure
{
    using StackExchange.Redis;

    public interface IRedisContext
    {
        IDatabase Context { get; set; }
    }
}
