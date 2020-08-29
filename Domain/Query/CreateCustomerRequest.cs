namespace MicroCredential.Domain.Query
{
    using MicroCredential.Domain.ViewModels;
    using MediatR;
    using System;

    public class CreateCustomerRequest : IRequest<bool>
    {
       public CreateCustomerRequest(CustomerViewModel customerViewModel)
        {
            CustomerViewModel = customerViewModel;
        }

        public CustomerViewModel CustomerViewModel { get; set; }
    }
}
