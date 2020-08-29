namespace MicroCredential.Domain.Query
{
    using MicroCredential.Domain.ViewModels;
    using MediatR;
    using System;

    public class GetCustomerQuery: IRequest<CustomerViewModel>
    {
       public GetCustomerQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
