namespace MicroCredential.Domain
{
    using AutoMapper;
    using MicroCredential.Domain.ViewModels;
    using MicroCredential.Infrastructure.Entity;
    using System;

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>()
                .ForMember(d => d.CustomerId, opt => opt.MapFrom(o => Guid.NewGuid()));
        }
    }
}
