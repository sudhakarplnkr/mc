namespace MicroCredential.Infrastructure
{
    using MicroCredential.Infrastructure.Entity;
    using Microsoft.Extensions.Caching.Distributed;
    using System;

    public class CustomerRedisContext : ICustomerRedisContext
    {
        private readonly IDistributedCache cache;
        private readonly CustomerDbContext customerDbContext;

        public CustomerRedisContext(IDistributedCache cache, CustomerDbContext customerDbContext)
        {
            this.cache = cache;
            this.customerDbContext = customerDbContext;
        }

        public Customer GetCustomer(Guid customerId)
        {
            var customer = cache.Get<Customer>($"{customerId}");
            if (customer == null)
            {
                customer = customerDbContext.Customers.Find(customerId);
                cache.Set($"{customer.CustomerId}", customer);
            }
            return customer;
        }

        public bool SetCustomer(Customer customer)
        {
            customerDbContext.Customers.Add(customer);
            customerDbContext.SaveChanges();
            cache.Set($"{customer.CustomerId}", customer);
            return true;
        }
    }
}
