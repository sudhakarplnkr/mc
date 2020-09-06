namespace MicroCredential.Infrastructure
{
    using FakeItEasy;
    using MicroCredential.Infrastructure.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using System;
    using Xunit;

    public class CustomerRedisContextTest
    {
        [Fact]
        public void GivenCustomerIdWhenGetCustomerThenReadFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: "customerdatabse")
                .Options;
            var expect = new Customer
            {
                Age = 35,
                City = "Chennai",
                Country = "India",
                Name = "Mark",
                State = "Tamil Nadu",
                CustomerId = Guid.NewGuid(),
            };
            var cache = A.Fake<IDistributedCache>();
            var customerDbContext = new CustomerDbContext(options);
            customerDbContext.Customers.Add(expect);
            customerDbContext.SaveChanges();
            var customerRedisContext = new CustomerRedisContext(cache, customerDbContext);

            // Act
            var response = customerRedisContext.GetCustomer(expect.CustomerId);

            // Assert
            Assert.Equal(expect.City, response.City);
        }

        [Fact]
        public void GivenCustomerWhenSaveCustomerThenSaveToDatabase()
        {
            // Arrange
            var cacheOption = Options.Create(new MemoryDistributedCacheOptions());
            var cache = new MemoryDistributedCache(cacheOption);
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: "customerdatabse")
                .Options;
            var expect = new Customer
            {
                Age = 35,
                City = "Chennai",
                Country = "India",
                Name = "Mark",
                State = "Tamil Nadu",
                CustomerId = Guid.NewGuid(),
            };
            var customerDbContext = new CustomerDbContext(options);
            var customerRedisContext = new CustomerRedisContext(cache, customerDbContext);

            // Act
            var response = customerRedisContext.SetCustomer(expect);
            var cachedCustomer = cache.Get<Customer>($"{expect.CustomerId}");

            // Assert
            Assert.True(response);
            Assert.Equal(cachedCustomer.Country, expect.Country);
        }
        
        [Fact]
        public void GivenCustomerIdWhenGetCustomerThenReadFromCache()
        {
            // Arrange
            var opts = Options.Create(new MemoryDistributedCacheOptions());
            var cache = new MemoryDistributedCache(opts);
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: "customerdatabse")
                .Options;
            var expect = new Customer
            {
                City = "Chennai",
                CustomerId = Guid.NewGuid(),
            };
            cache.Set($"{expect.CustomerId}", expect);
            var customerDbContext = new CustomerDbContext(options);
            var customerRedisContext = new CustomerRedisContext(cache, customerDbContext);

            // Act
            var response = customerRedisContext.GetCustomer(expect.CustomerId);

            // Assert
            Assert.Equal(expect.City, response.City);
        }
    }
}
