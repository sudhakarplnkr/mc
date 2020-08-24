namespace CustomerApi.Query
{
    using System.Threading;
    using System.Threading.Tasks;
    using CustomerApi.ViewModels;
    using MediatR;

    public class CustomerHandler : IRequestHandler<CustomerQuery, CustomerViewModel>
    {
        public Task<CustomerViewModel> Handle(CustomerQuery request, CancellationToken cancellationToken)
        {
            // TODO: hard code to be removed
            var customerViewModel = new CustomerViewModel
            {
                Age = 35,
                City = "Chennai",
                Country = "India",
                Name = "Mark",
                State = "Tamil Nadu",
            };
            return Task.Run(() => customerViewModel);
        }
    }
}
