namespace MicroCredential.CustomerApi.Tests
{
    using FakeItEasy;
    using MicroCredential.CustomerApi.Controllers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class StartupTest
    {
        [Fact]
        public void GivenStartupWhenApplicationStartThenControllerToBeNotNull()
        {
            // Arrange
            var configuration = A.Fake<IConfiguration>();
            var service = new ServiceCollection();
            service.AddTransient<CustomerController>();
            var startup = new Startup(configuration);
            
            // Act
            startup.ConfigureServices(service);

            // Assert
            var serviceProvider = service.BuildServiceProvider();
            var controller = serviceProvider.GetService<CustomerController>();
            Assert.NotNull(controller);
        }
    }
}
