using AutoMapper;
using FakeItEasy;
using MicroCredential.Domain;
using MicroCredential.Domain.Handler;
using MicroCredential.Domain.Query;
using MicroCredential.Domain.ViewModels;
using MicroCredential.Infrastructure;
using MicroCredential.Infrastructure.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
    public class CustomerHandlerTest
    {
        [Fact]
        public async Task GiveGetCustomerQueryWhenHandleThenCustomerDetails()
        {
            // Arrange
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
            var redis = A.Fake<ICustomerRedisContext>();
            A.CallTo(() => redis.GetCustomer(expect.CustomerId)).Returns(expect);
            var handle = new CustomerHandler(mapper, redis);

            // Act
            var response = await handle.Handle(new GetCustomerRequest(expect.CustomerId), A.Dummy<CancellationToken>()).ConfigureAwait(false);

            // Assert
            Assert.Equal(35, response.Age);
        }

        [Fact]
        public async Task GiveCustomerWhenHandleThenCustomerToBeAdded()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var redis = A.Fake<ICustomerRedisContext>();
            var handle = new CustomerHandler(mapper, redis);

            // Act
            var response = await handle.Handle(new CreateCustomerRequest(new CustomerViewModel()), A.Dummy<CancellationToken>()).ConfigureAwait(false);

            // Assert
            A.CallTo(() => redis.SetCustomer(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GiveNullCustomerWhenHandleThenThrowException()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var redis = A.Fake<ICustomerRedisContext>();
            var handle = new CustomerHandler(mapper, redis);

            // Act
            Task act = handle.Handle(null as CreateCustomerRequest, A.Dummy<CancellationToken>());

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => act);
        }
    }
}
