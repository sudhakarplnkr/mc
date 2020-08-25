namespace MicroCredential.Domain.Query
{
    using MicroCredential.ViewModels;
    using MediatR;

    public class CustomerQuery: IRequest<CustomerViewModel>
    {
       public CustomerQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
