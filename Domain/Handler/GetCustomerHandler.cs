namespace MicroCredential.Domain.Handler
{
    using System.Threading;
    using System.Threading.Tasks;
    using MicroCredential.ViewModels;
    using MediatR;
    using MicroCredential.Domain.Query;

    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerViewModel>
    {
        public Task<CustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
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
