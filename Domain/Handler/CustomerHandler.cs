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
    using System.Linq;

    public class CustomerHandler : IRequestHandler<GetCustomerRequest, CustomerViewModel>,
                                   IRequestHandler<CreateCustomerRequest, bool>
    {
        private readonly IMapper mapper;
        private readonly CustomerDbContext customerDbContext;

        public CustomerHandler(IMapper mapper, CustomerDbContext dbContext)
        {
            this.mapper = mapper;
            this.customerDbContext = dbContext;
        }

        public Task<CustomerViewModel> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var customerDetail = customerDbContext.Customers.FirstOrDefault(u => u.CustomerId == request.Id);
            return Task.Run(() => mapper.Map<CustomerViewModel>(customerDetail));
        }

        public async Task<bool> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        { 
            var customerDetail = mapper.Map<Customer>(request.CustomerViewModel);
            await customerDbContext.Customers.AddAsync(customerDetail).ConfigureAwait(false);
            return await customerDbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}
