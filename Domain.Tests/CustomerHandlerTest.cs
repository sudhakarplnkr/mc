using AutoMapper;
using FakeItEasy;
using MicroCredential.Domain.Handler;
using MicroCredential.Domain.Query;
using MicroCredential.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using MicroCredential.Infrastructure.Entity;
using MicroCredential.Domain;
using MicroCredential.Domain.ViewModels;

namespace Domain.Tests
{
    [TestClass]
    public class CustomerHandlerTest
    {
        [TestMethod]
        public async Task GiveGetCustomerQueryWhenHandleThenCustomerDetails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: "customerdatabse")
                .Options;
            var context = new CustomerDbContext(options);
            var expect = new Customer
            {
                Age = 35,
                City = "Chennai",
                Country = "India",
                Name = "Mark",
                State = "Tamil Nadu",
                CustomerId = Guid.NewGuid(),
            };
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
            });
            var mapper = mockMapper.CreateMapper();
            context.Customers.Add(expect);
            context.SaveChanges();
            var handle = new CustomerHandler(mapper, context);

            // Act
            var response = await handle.Handle(new GetCustomerRequest(expect.CustomerId), A.Dummy<CancellationToken>()).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(35, response.Age);
        }

        [TestMethod]
        public async Task GiveCustomerWhenHandleThenCustomerToBeAdded()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: "customerdatabse")
                .Options;
            var context = new CustomerDbContext(options);
            var customerViewModel = new CustomerViewModel
            {
                Age = 35,
                City = "Chennai",
                Country = "India",
                Name = "Mark",
                State = "Tamil Nadu",
            };
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
            });
            var mapper = mockMapper.CreateMapper();
            context.SaveChanges();
            var handle = new CustomerHandler(mapper, context);

            // Act
            var response = await handle.Handle(new CreateCustomerRequest(customerViewModel), A.Dummy<CancellationToken>()).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(true, response);
        }
    }
}
