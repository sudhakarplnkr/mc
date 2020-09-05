namespace MicroCredential.Domain.Handler
{
    using System.Threading;
    using System.Threading.Tasks;
    using MicroCredential.Domain.ViewModels;
    using MediatR;
    using MicroCredential.Domain.Query;
    using MicroCredential.Infrastructure.Entity;
    using MicroCredential.Infrastructure;
    using AutoMapper;

    public class CustomerHandler : IRequestHandler<GetCustomerRequest, CustomerViewModel>,
                                   IRequestHandler<CreateCustomerRequest, bool>
    {
        private readonly IMapper mapper;
        private readonly ICustomerRedisContext customerRedisContext;

        public CustomerHandler(IMapper mapper, ICustomerRedisContext customerRedisContext)
        {
            this.mapper = mapper;
            this.customerRedisContext = customerRedisContext;
        }

        public async Task<CustomerViewModel> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var customerDetail = customerRedisContext.GetCustomer(request.Id);
            var viewModel = mapper.Map<CustomerViewModel>(customerDetail);
            return await Task.Run(() => viewModel);
        }

        public async Task<bool> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        { 
            var customerDetail = mapper.Map<Customer>(request.CustomerViewModel);
            return await Task.Run(() => customerRedisContext.SetCustomer(customerDetail));
        }
    }
}
