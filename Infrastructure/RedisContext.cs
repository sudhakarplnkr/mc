namespace MicroCredential.Infrastructure
{
    using MicroCredential.Infrastructure.Entity;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;

    public class RedisContext : IRedisContext
    {
        public RedisContext(string connection)
        {
            var redis = ConnectionMultiplexer.Connect(connection);
            Context = redis.GetDatabase();
        }

        public RedisContext(string connection, CustomerDbContext customerDbContext)
        {
            var redis = ConnectionMultiplexer.Connect(connection);
            Context = redis.GetDatabase();
        }

        public IDatabase Context { get; set; }

        public IEnumerable<Customer> Customers { get { return Context.Get<IEnumerable<Customer>>("Customers"); } }

        public void SetCustomers(IEnumerable<Customer> customers) 
        {
            Context.Set("Customers", customers, TimeSpan.FromDays(1));
        }
    }
}
