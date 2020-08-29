using AutoMapper;
using FakeItEasy;
using MicroCredential.Domain.Handler;
using MicroCredential.Domain.Query;
using MicroCredential.Domain.ViewModels;
using MicroCredential.Infrastructure;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tests
{
    [TestClass]
    public class CustomerHandlerTest
    {
        [TestMethod]
        public async Task GiveGetCustomerQueryWhenHandleThenCustomerDetails()
        {
            // Arrange
            var handle = new CustomerHandler(A.Fake<ICustomerDatabaseSettings>(), A.Fake<IMapper>());
            var expect = new CustomerViewModel
            {
                Age = 35,
                City = "Chennai",
                Country = "India",
                Name = "Mark",
                State = "Tamil Nadu",
            };

            // Act
            var response = await handle.Handle(new GetCustomerQuery(Guid.NewGuid()), A.Dummy<CancellationToken>()).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(expect.Age, response.Age);
        }
    }
}
