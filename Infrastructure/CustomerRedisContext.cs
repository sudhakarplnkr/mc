namespace MicroCredential.Infrastructure
{
    using MicroCredential.Infrastructure.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerRedisContext : ICustomerRedisContext
    {
        private const string Key = "Customers";
        private readonly IRedisContext redisContext;
        private readonly CustomerDbContext customerDbContext;

        public CustomerRedisContext(IRedisContext redisContext, CustomerDbContext customerDbContext)
        {
            this.redisContext = redisContext;
            this.customerDbContext = customerDbContext;
            GetCustomers();
        }

        public List<Customer> Customers { get; set; }

        public List<Customer> GetCustomers()
        {
            Customers = redisContext.Context.Get<List<Customer>>(Key) ?? new List<Customer>();
            if (Customers.Count == 0)
            {
                SetCustomer(customerDbContext.Customers.ToList());
            }
            return Customers;
        }

        public bool SetCustomer(List<Customer> customers)
        {
            if (customers.Count > 0)
            {
                redisContext.Context.Set(Key, customers, TimeSpan.FromDays(1));
            }
            return true;
        }

        public bool SetCustomer(Customer customer)
        {
            customerDbContext.Customers.Add(customer);
            customerDbContext.SaveChanges();
            Customers.Add(customer);
            return SetCustomer(Customers);
        }
    }
}
