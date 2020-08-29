namespace MicroCredential.Domain.Handler
{
    using AutoMapper;
    using MicroCredential.Domain.ViewModels;
    using MicroCredential.Infrastructure.Entity;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;
    using System;

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            BsonClassMap.RegisterClassMap<Customer>(
    map =>
    {
        map.AutoMap();
        map.MapProperty(x => x.CustomerId)
            .SetSerializer(new GuidSerializer(BsonType.String));
    });

            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>()
               .ForMember(cus => cus.CustomerId, act => act.MapFrom(src => Guid.NewGuid()));
        }
    }
}
