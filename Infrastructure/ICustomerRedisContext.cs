namespace MicroCredential.Infrastructure
{
    using MicroCredential.Infrastructure.Entity;
    using System.Collections.Generic;

    public interface ICustomerRedisContext
    {
        List<Customer> Customers { get; set; }

        bool SetCustomer(List<Customer> customers);

        bool SetCustomer(Customer customer);
    }
}
