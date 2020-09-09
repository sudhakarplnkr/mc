namespace MicroCredential.Domain.Query
{
    using MicroCredential.Domain.ViewModels;
    using MediatR;
    using System;

    public class GetCustomerRequest: IRequest<CustomerViewModel>
    {
       public GetCustomerRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
