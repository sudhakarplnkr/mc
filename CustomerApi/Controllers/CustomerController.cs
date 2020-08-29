namespace MicroCredential.CustomerApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using GuardAgainstLib;
    using MediatR;
    using MicroCredential.Domain.Query;
    using MicroCredential.Domain.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{customerId}")]
        public async Task<CustomerViewModel> Get(Guid customerId)
        {
            GuardAgainst.ArgumentBeingEmpty(customerId, "Invalid customer id");

            return await mediator.Send(new GetCustomerQuery(customerId)).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<bool> Post(CustomerViewModel customerViewModel)
        {
            GuardAgainst.ArgumentBeingNull(customerViewModel, "customer detail can't be null");

            return await mediator.Send(new CreateCustomerRequest(customerViewModel)).ConfigureAwait(false);
        }
    }
}
