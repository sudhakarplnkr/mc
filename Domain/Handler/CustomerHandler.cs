namespace MicroCredential.Domain.Handler
{
    using System.Threading;
    using System.Threading.Tasks;
    using MicroCredential.Domain.ViewModels;
    using MediatR;
    using MicroCredential.Domain.Query;
    using MicroCredential.Infrastructure.Entity;
    using MongoDB.Driver;
    using MicroCredential.Infrastructure;
    using AutoMapper;
    using System;

    public class CustomerHandler : IRequestHandler<GetCustomerQuery, CustomerViewModel>, 
                                   IRequestHandler<CreateCustomerRequest, bool>
    {
        private readonly IMongoCollection<Customer> customer;
        private readonly IMapper mapper;

        public CustomerHandler(ICustomerDatabaseSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            customer = database.GetCollection<Customer>(settings.CustomerCollectionName);
            this.mapper = mapper;
        }

        public Task<CustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customerDetail = customer.Find<Customer>(u => u.CustomerId == request.Id).FirstOrDefault();
            return Task.Run(() => mapper.Map<CustomerViewModel>(customerDetail));
        }

        public Task<bool> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        { 
            var customerDetail = mapper.Map<Customer>(request.CustomerViewModel);
            customer.InsertOne(customerDetail);
            return Task.FromResult(true);
        }
    }
}
