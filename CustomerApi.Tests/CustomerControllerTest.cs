namespace MicroCredential.CustomerApi.Tests
{
    using FakeItEasy;
    using MediatR;
    using MicroCredential.CustomerApi.Controllers;
    using MicroCredential.Domain.Query;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class CustomerControllerTest
    {
        [Fact]
        public async Task GivenInvalidCustomerIdWhenGetThenThrow()
        {
            // Arrange
            var controller = new CustomerController(A.Fake<IMediator>());

            // Act
            async Task act() => await controller.Get(Guid.Empty).ConfigureAwait(false);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task GivenCustomerIdWhenGetThenSendGetCustomerQueryRequest()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            var controller = new CustomerController(mediator);
            
            // Act
            var response = await controller.Get(Guid.NewGuid()).ConfigureAwait(false);

            // Assert
            A.CallTo(() => mediator.Send(A<GetCustomerQuery>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
