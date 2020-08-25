using FakeItEasy;
using MicroCredential.Domain.Handler;
using MicroCredential.Domain.Query;
using MicroCredential.ViewModels;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var handle = new GetCustomerHandler();
            var expect = new CustomerViewModel
            {
                Age = 35,
                City = "Chennai",
                Country = "India",
                Name = "Mark",
                State = "Tamil Nadu",
            };

            // Act
            var response = await handle.Handle(new GetCustomerQuery(1), A.Dummy<CancellationToken>()).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(expect.Age, response.Age);
        }
    }
}
