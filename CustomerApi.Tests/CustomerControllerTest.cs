namespace MicroCredential.CustomerApi.Tests
{
    using FakeItEasy;
    using MediatR;
    using MicroCredential.CustomerApi.Controllers;
    using MicroCredential.Domain.Query;
    using MicroCredential.Domain.ViewModels;
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
            A.CallTo(() => mediator.Send(A<GetCustomerRequest>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GivenInvalidCustomerWhenPostThenThrowAurgumentException()
        {
            // Arrange
            var controller = new CustomerController(A.Fake<IMediator>());

            // Act
            async Task act() => await controller.Post(null).ConfigureAwait(false);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task GivenCustomerWhenGetThenSendCreateCustomerRequest()
        {
            // Arrange
            var customerViewModel = A.Fake<CustomerViewModel>();
            var mediator = A.Fake<IMediator>();
            var controller = new CustomerController(mediator);

            // Act
            var response = await controller.Post(customerViewModel).ConfigureAwait(false);

            // Assert
            A.CallTo(() => mediator.Send(A<CreateCustomerRequest>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
