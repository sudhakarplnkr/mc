namespace WebApplication1.Controllers
{
    using System.Threading.Tasks;
    using CustomerApi.Query;
    using CustomerApi.ViewModels;
    using MediatR;
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
            return await mediator.Send(new CustomerQuery(id)).ConfigureAwait(false);
        }
    }
}
