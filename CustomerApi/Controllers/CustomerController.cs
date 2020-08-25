namespace MicroCredential.CustomerApi.Controllers
{
    using System.Threading.Tasks;
    using GuardAgainstLib;
    using MediatR;
    using MicroCredential.Domain.Query;
    using MicroCredential.ViewModels;
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

        [HttpGet("{id}")]
        public async Task<CustomerViewModel> Get(int id)
        {
            GuardAgainst.ArgumentBeingInvalid(id <= 0, "Invalid customer id");

            return await mediator.Send(new GetCustomerQuery(id)).ConfigureAwait(false);
        }
    }
}
