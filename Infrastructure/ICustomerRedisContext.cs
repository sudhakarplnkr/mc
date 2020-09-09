namespace MicroCredential.Infrastructure
{
    using MicroCredential.Infrastructure.Entity;
    using System;

    public interface ICustomerRedisContext
    {
        Customer GetCustomer(Guid customerId);

        bool SetCustomer(Customer customer);
    }
}
