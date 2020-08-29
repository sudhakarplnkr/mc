namespace MicroCredential.Domain.Query
{
    using MicroCredential.ViewModels;
    using MediatR;

    public class GetCustomerQuery: IRequest<CustomerViewModel>
    {
       public GetCustomerQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
